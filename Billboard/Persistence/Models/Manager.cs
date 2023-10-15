using Persistence.Enums;

namespace Persistence.Models;

public class Manager
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string Password { get; set; }
    public required string FirstName { get; init; }
    public required string MiddleName { get; init; }
    public required string LastName { get; init; }
    public ManagerStatusId StatusId { get; set; }
    public ManagerStatus? ManagerStatus { get; private set; }
}