using Persistence.Enums;

namespace Persistence.Models;

public class Billboard
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Address { get; init; }
    public required BillboardTypeId TypeId { get; set; }
    public BillboardType? BillboardType { get; set; }
    public required ArchiveStatusId ArchiveStatusId { get; set; }
    public ArchiveStatus? ArchiveStatus { get; set; }
    public required BillboardSurface BillboardSurface { get; set; }
    public required decimal Width { get; init; }
    public required decimal Height { get; init; }
    public decimal Penalty { get; init; }
    public ICollection<Discount> Discounts { get; init; } = new List<Discount>();
    public ICollection<Picture> Pictures { get; init; } = new List<Picture>();
    public required GroupOfTariffs GroupOfTariffs { get; set; }
}