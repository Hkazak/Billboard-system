using Persistence.Enums;

namespace Persistence.Models;

public class PaymentStatus
{
    public required PaymentStatusId Id { get; init; }
    public required string Name { get; init; }
}