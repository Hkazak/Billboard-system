namespace Contracts.Responses;

public record OrderPriceResponse
{
    public required decimal ProductPrice { get; init; }
    public required decimal RentPrice { get; init; }
    public required decimal PenaltyPrice { get; init; }
}