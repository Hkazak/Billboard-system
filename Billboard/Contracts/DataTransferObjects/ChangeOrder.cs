namespace Contracts.DataTransferObjects;

public record ChangeOrder
{
    public required Guid OrderId { get; init; }
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
    public required Guid RequestSenderId { get; init; }
}