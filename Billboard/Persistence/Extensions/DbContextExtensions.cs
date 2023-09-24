using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Extensions;

public static class DbContextExtensions
{
    public static Task<bool> IsUniqueEmailAsync(this BillboardContext context, string email,
        CancellationToken cancellationToken = default)
    {
        return context.Users.AllAsync(e => e.Email != email, cancellationToken);
    }
}