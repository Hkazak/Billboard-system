namespace Contracts.Requests;

public record AddDiscountRequest
{
    public required string Name { get; init; }
    public required decimal SalesOf { get; init; }
}