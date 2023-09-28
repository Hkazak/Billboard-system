using Contracts.Responses;
using Persistence.Models;

namespace Application.Extensions;

public static class MapperExtensions
{
    public static UserResponse CreateResponse(this User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Role = user.RoleId.ToString()
        };
    }

    public static ManagerResponse CreateResponse(this Manager user)
    {
        return new ManagerResponse
        {
            Id = user.Id,
            Email = user.Email,
            FullName = $"{user.LastName} {user.FirstName} {user.MiddleName}",
            Phone = user.Phone,
        };
    }
}