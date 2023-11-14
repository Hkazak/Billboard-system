namespace Persistence.Models;

public class Picture
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required string Source { get; init; }
}