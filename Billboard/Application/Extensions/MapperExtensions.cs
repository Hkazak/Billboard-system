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

    public static ManagerResponse CreateResponse(this Manager manager)
    {
        return new ManagerResponse
        {
            Id = manager.Id,
            Email = manager.Email,
            FirstName = manager.FirstName,
            MiddleName = manager.MiddleName,
            LastName = manager.LastName,
            Phone = manager.Phone,
        };
    }

    public static BillboardResponse CreateResponse(this Billboard billboard)
    {
        return new BillboardResponse
        {
            Id = billboard.Id,
            Name = billboard.Name,
            Description = billboard.Description,
            Address = billboard.Address,
            BillboardType = billboard.TypeId.ToString(),
            BillboardSurface = billboard.BillboardSurface.Surface,
            Width = billboard.Width,
            Height = billboard.Height,
            Penalty = billboard.Penalty,
            PictureSource = billboard.Pictures.Select(e => e.Source).ToList(),
            GroupOfTariffs = billboard.GroupOfTariffs.CreateResponse(),
        };
    }

    public static TariffResponse CreateResponse(this Tariff tariff)
    {
        return new TariffResponse
        {
            Id = tariff.Id,
            Title = tariff.Title,
            StartTime = tariff.StartTime,
            EndTime = tariff.EndTime,
            Price = tariff.Price
        };
    }

    public static GroupOfTariffsResponse CreateResponse(this GroupOfTariffs groupOfTariffs)
    {
        return new GroupOfTariffsResponse
        {
            Id = groupOfTariffs.Id,
            Name = groupOfTariffs.Name,
            Tariffs = groupOfTariffs.Tariffs.Select(e => e.CreateResponse()).ToList()
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
    
    public static AuthenticationClaims CreateClaims(this Manager manager)
    {
        return new AuthenticationClaims
        {
            Id = manager.Id,
            Email = manager.Email,
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