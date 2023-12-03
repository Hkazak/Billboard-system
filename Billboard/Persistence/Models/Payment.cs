using Persistence.Enums;

namespace Persistence.Models;

public class Payment
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required Guid OrderId { get; init; }
    public required string ExternalOrderId { get; init; }
    public required PaymentStatusId PaymentStatusId { get; set; }
    public required DateTime DueDate { get; init; }
    public Order? Order { get; private set; }
    public PaymentStatus? Status { get; private set; }
}