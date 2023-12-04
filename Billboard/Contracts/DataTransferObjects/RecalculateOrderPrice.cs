namespace Contracts.DataTransferObjects;

public record RecalculateOrderPrice
{
    public required Guid OrderId { get; init; }
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
}