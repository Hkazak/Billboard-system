using Persistence.Enums;

namespace Persistence.Models;

public class PriceRule
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required Guid BillboardSurfaceId { get; init; }
    public BillboardSurface? BillboardSurface { get; set; }
    public required BillboardTypeId BillboardTypeId { get; init; }
    public BillboardType? BillboardType { get; set; }
    public required decimal Price { get; init; }
}