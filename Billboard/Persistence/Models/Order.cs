using Persistence.Enums;

namespace Persistence.Models;

public class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required decimal RentPrice { get; set; }
    public required decimal PenaltyPrice { get; set; }
    public required decimal ProductPrice { get; set; }
    public required Billboard Billboard { get; init; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required User User { get; init; }
    public Guid? DiscountId { get; set; }
    public Discount? Discount { get; private set; }
    public required Tariff SelectedTariff { get; init; }
    public OrderStatusId StatusId { get; set; }
    public OrderStatus? OrderStatus { get; private set; }
}