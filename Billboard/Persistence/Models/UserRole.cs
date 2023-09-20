using Persistence.Enums;

namespace Persistence.Models;

public class UserRole
{
    public required UserRoleId Id { get; init; }
    public required string Role { get; init; }
}