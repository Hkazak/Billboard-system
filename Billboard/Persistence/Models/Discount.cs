using Persistence.Enums;

namespace Persistence.Models;

public class Discount
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required decimal SalesOf { get; set; }
    public required ArchiveStatusId ArchiveStatusId { get; set; }
    public ArchiveStatus? ArchiveStatus { get; set; }
}