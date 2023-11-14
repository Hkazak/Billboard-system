namespace Contracts.Requests;

public record AddBillboardRequest
{
    public required string Name { get; init; }
    public required string Address { get; init; }
    public required string Description { get; init; }
    public required Guid GroupOfTariffs { get; init; }
    public required string BillboardType { get; init; }
    public required Guid BillboardSurfaceId { get; init; }
    public decimal Penalty { get; init; }
    public required decimal Height { get; init; }
    public required decimal Width { get; init; }
    public string? PictureSource { get; init; }
}