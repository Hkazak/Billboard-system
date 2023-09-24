using Contracts.Responses;
using Persistence.Models;

namespace Application.Services;

public interface IAuthenticationService
{
    AuthTokenResponse GenerateJwtToken(User user);
}