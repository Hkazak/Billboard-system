using Persistence.Enums;

namespace Persistence.Models;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Password { get; set; }
    public required UserRoleId RoleId { get; init; }
    public UserRole? Role { get; private set; }
    public ICollection<Order> Orders { get; init; } = new List<Order>();
}