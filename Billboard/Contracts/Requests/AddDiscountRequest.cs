namespace Contracts.Requests;

public record AddDiscountRequest
{
    public required string Name { get; init; }
    public required decimal SalesOf { get; init; }
    public required int MinRentCount { get; init; }
    public required string EndDate { get; init; }
}