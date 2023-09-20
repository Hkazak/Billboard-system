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

    private bool Equals(User other)
    {
        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj.GetType() == GetType() && Equals((User) obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}