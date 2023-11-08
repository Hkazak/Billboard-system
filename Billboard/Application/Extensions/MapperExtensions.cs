using Application.CQRS.Commands;
using Application.InternalModels;
using Contracts.DataTransferObjects;
using Contracts.Requests;
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
            FirstName = user.FirstName,
            MiddleName = user.MiddleName,
            LastName = user.LastName,
            Phone = user.Phone,
        };
    }

    public static AuthenticationClaims CreateClaims(this User user)
    {
        return new AuthenticationClaims
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.RoleId.ToString()
        };
    }
    
    public static AuthenticationClaims CreateClaims(this Manager user)
    {
        return new AuthenticationClaims
        {
            Id = user.Id,
            Email = user.Email,
            Role = "Manager"
        };
    }

    public static ChangePassword CreateUserPasswordData(this ChangePasswordRequest request, Guid id)
    {
        return new ChangePassword
        {
            Id = id,
            OldPassword = request.CurrentPassword,
            NewPassword = request.NewPassword
        };
    }

    public static CodeConfirmation CreateResetPasswordData(this ResetPasswordRequest request)
    {
        return new CodeConfirmation
        {
            Email = request.Email,
            NewPassword = request.NewPassword,
            ConfirmationCode = request.ConfirmationCode
        };
    }
}