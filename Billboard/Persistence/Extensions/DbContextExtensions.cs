using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;

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

    public static async Task<bool> IsUniquePriceRuleAsync(this BillboardContext context, Guid billboardSurfaceId,
        BillboardTypeId billboardTypeId, CancellationToken cancellationToken = default)
    {
        var hasSame = await context.PriceRules.AnyAsync(e => e.BillboardSurfaceId == billboardSurfaceId && e.BillboardTypeId == billboardTypeId, cancellationToken);
        return !hasSame;
    }

    public static async Task<bool> IsNotIntersectAsync(this BillboardContext context, DateTime startDate,
        DateTime endDate,Guid billboardId, Guid tariffId, CancellationToken cancellationToken = default)
    {
        var isIntersect = await context.Orders.Where(e => e.BillboardId == billboardId && e.TariffId == tariffId)
            .AnyAsync(e => e.StartDate.Date <= startDate.Date && startDate.Date <= e.EndDate.Date 
                           || e.StartDate.Date <= endDate.Date && endDate.Date <= e.EndDate.Date 
                           || startDate.Date  <= e.StartDate.Date && e.StartDate.Date <= endDate.Date 
                           || startDate.Date <= e.EndDate.Date && e.EndDate.Date <= endDate.Date , cancellationToken);
        return !isIntersect;
    }
}