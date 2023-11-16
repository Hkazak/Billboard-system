using Persistence.Enums;

namespace Persistence.Models;

public class Tariff
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required string Title { get; init; }
    public required TimeSpan StartTime { get; set; }
    public required TimeSpan EndTime { get; set; }
    public required decimal Price { get; set; }
    public required ArchiveStatusId ArchiveStatusId { get; set; }
    public ArchiveStatus? ArchiveStatus { get; set; }
    public ICollection<GroupOfTariffs> Groups { get; init; } = new List<GroupOfTariffs>();
}