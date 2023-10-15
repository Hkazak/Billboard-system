using Application.InternalModels;
using Contracts.Responses;

namespace Application.Services;

public interface IAuthenticationService
{
    AuthTokenResponse GenerateJwtToken(AuthenticationClaims claims);
}