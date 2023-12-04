namespace Application.InternalModels.PaymentService.Requests;

public record UpdateOrder
{
    public required long Amount { get; init; }
}