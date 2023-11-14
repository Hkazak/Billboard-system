using Persistence.Enums;

namespace Persistence.Models;

public class OrderStatus
{
    public required OrderStatusId Id { get; init; }
    public required string Status { get; init; }
}