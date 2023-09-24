using System.Security.Claims;
using Contracts.Constants;
using Contracts.Exceptions;

namespace Presentation.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal context)
    {
        var uid = context.FindFirstValue(CustomClaimTypes.UserId);
        if (uid is null || !Guid.TryParse(uid, out var userId))
        {
            throw new NotPermissionsException("User not authorized");
        }

        return userId;
    }
}