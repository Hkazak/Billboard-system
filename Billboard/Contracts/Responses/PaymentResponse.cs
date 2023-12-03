namespace Contracts.Responses;

public record PaymentResponse
{
    public required Guid Id { get; init; }
    public required string CheckoutUrl { get; init; }
}