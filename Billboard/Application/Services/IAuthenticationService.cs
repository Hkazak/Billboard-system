using Contracts.Responses;
using Persistence.Models;

namespace Application.Services;

public interface IAuthenticationService
{
    // TODO add implementation, make method async if it needs
    AuthTokenResponse GenerateJwtToken(User user);
}