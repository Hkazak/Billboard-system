using Persistence.Enums;

namespace Persistence.Models;

public class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required decimal ProductPrice { get; init; }
    public required GroupOfTariffs GroupOfTariffs { get; init; }
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
    public IEnumerable<Discount> Discounts { get; set; }
    public OrderStatusId StatusId { get; init; }
    public OrderStatus? OrderStatus { get; private set; }
}