using Persistence.Enums;

namespace Persistence.Models;

public class Discount
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required string Name { get; init; }
    public required decimal SalesOf { get; init; }
    public required int MinRentCount { get; init; }
    public required DateTime EndDate { get; init; }
    public required ArchiveStatusId ArchiveStatusId { get; set; }
    public ArchiveStatus? ArchiveStatus { get; set; }
}