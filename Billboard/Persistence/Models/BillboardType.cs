using Persistence.Enums;

namespace Persistence.Models;

public class BillboardType
{
    public required BillboardTypeId Id { get; init; }
    public required string Type { get; init; }
}