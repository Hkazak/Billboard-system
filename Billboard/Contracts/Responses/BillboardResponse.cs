namespace Contracts.Responses;

public record BillboardResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Address { get; init; }
    public required string BillboardType { get; init; }
    public required string BillboardSurface { get; init; }
    public required decimal Width { get; init; }
    public required decimal Height { get; init; }
    public required decimal Penalty { get; init; }
    public required ICollection<string> PictureSource { get; init; }
    public required ICollection<DiscountResponse> Discounts { get; init; }
    public required GroupOfTariffsResponse GroupOfTariffs { get; init; }
}