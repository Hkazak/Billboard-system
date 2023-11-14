using Persistence.Enums;

namespace Persistence.Models;

public class ArchiveStatus
{
    public required ArchiveStatusId Id { get; init; }
    public required string Status { get; init; }
}