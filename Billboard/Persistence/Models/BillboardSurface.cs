namespace Persistence.Models;

public class BillboardSurface
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required string Surface { get; init; }
}