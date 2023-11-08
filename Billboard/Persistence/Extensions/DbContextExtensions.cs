﻿using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Extensions;

public static class DbContextExtensions
{
    public static async Task<bool> IsUniqueEmailAsync(this BillboardContext context, string email,
        CancellationToken cancellationToken = default)
    {
        var isUniqueEmailManager = await context.Managers.AllAsync(e => e.Email != email, cancellationToken);
        var isUniqueEmailUser = await context.Users.AllAsync(e => e.Email != email, cancellationToken);
        return isUniqueEmailManager && isUniqueEmailUser; 
    }

    public static async Task<bool> IsUniquePhoneAsync(this BillboardContext context, string phone,
        CancellationToken cancellationToken = default)
    {
        var isUniquePhone = await context.Managers.AllAsync(e => e.Phone != phone, cancellationToken);
        return isUniquePhone;
    }
}