namespace Contracts.Responses;

public record BookedOrderResponse
{
    public required Guid OrderId { get; init; }
    public required IEnumerable<DateTime> BookedDates { get; init; }
}