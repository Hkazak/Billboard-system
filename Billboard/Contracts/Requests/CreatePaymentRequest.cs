namespace Contracts.Requests;

public record CreatePaymentRequest
{
    public required Guid OrderId { get; init; }
}