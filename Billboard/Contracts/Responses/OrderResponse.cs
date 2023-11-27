namespace Contracts.Responses;

public record OrderResponse
{
    public required Guid Id { get; init; }
    public required BillboardResponse Billboard { get; init; }
    public required TariffResponse Tariff { get; init; }
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
    public required decimal RentPrice { get; init; }
    public required decimal ProductPrice { get; init; }
    public required decimal PenaltyPrice { get; init; }
    public required UserResponse User { get; init; }
    public DiscountResponse? Discount { get; init; }
}