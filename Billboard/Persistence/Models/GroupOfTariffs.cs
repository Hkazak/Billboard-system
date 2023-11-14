using Persistence.Enums;

namespace Persistence.Models;

public class GroupOfTariffs
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required ICollection<Tariff> Tariffs { get; init; } = new List<Tariff>();
    public required ArchiveStatusId ArchiveStatusId { get; set; }
    public ArchiveStatus? ArchiveStatus { get; set; }
}