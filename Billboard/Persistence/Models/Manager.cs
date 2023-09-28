namespace Persistence.Models;

public class Manager
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string Password { get; init; }
    public required string FirstName { get; init; }
    public required string MiddleName { get; init; }
    public required string LastName { get; init; }
}