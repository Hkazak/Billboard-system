namespace Contracts.Requests;

public record AddPriceRuleRequest
{
    public required Guid BillboardSurfaceId { get; init; }
    public required string BillboardType { get; init; }
    public required decimal Price { get; init; }
}