using Persistence.Enums;

namespace Persistence.Models;

public class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required decimal RentPrice { get; set; }
    public required decimal PenaltyPrice { get; set; }
    public required decimal ProductPrice { get; set; }    
    public required DateTime StartDate { get; set; } 
    public required DateTime EndDate { get; set; }
    public required Guid BillboardId { get; init; }
    public required Guid SelectedTariffId { get; init; }
    public required Guid UserId { get; init; }
    public Billboard? Billboard { get;  set; }
    public Tariff? SelectedTariff { get;  set; }
    public User? User { get;  set; }
    public Guid? DiscountId { get; set; }
    public Discount? Discount { get; set; }
    public OrderStatusId StatusId { get; set; }
    public OrderStatus? OrderStatus { get; private set; }
    public ICollection<Picture> Pictures { get; init; } = new List<Picture>();
}