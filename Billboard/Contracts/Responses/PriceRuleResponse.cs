namespace Contracts.Responses;

public record PriceRuleResponse
{
    public required Guid Id { get; init; }
    public required BillboardSurfaceResponse BillboardSurface { get; init; }
    public required string BillboardType { get; init; }
    public required decimal Price { get; init; }
}