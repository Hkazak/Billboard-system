using Persistence.Enums;

namespace Persistence.Models;

public class ManagerStatus
{
    public required ManagerStatusId Id { get; init; }
    public required string Status { get; init; }
}