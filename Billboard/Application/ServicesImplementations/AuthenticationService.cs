using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Services;
using Contracts.Configurations;
using Contracts.Constants;
using Contracts.Responses;
using Microsoft.IdentityModel.Tokens;
using Persistence.Models;

namespace Application.ServicesImplementations;

public class AuthenticationService : IAuthenticationService
{
    private readonly JwtConfiguration _config;

    public AuthenticationService(JwtConfiguration config)
    {
        _config = config;
    }

    public AuthTokenResponse GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(CustomClaimTypes.UserId, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.RoleId.ToString())
        };
        var token = new JwtSecurityToken(_config.Issuer,
            _config.Audience,
            claims,
            expires: DateTime.UtcNow.AddMonths(1),
            signingCredentials: credentials);

        var jwtToken = new JwtSecurityTokenHandler();
        var authJwtToken = new AuthTokenResponse
        {
            AccessToken = jwtToken.WriteToken(token)
        };

        return authJwtToken;
    }
}