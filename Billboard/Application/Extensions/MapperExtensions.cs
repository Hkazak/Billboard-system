using Application.InternalModels;
using Contracts.Constants;
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

    public static PriceRuleResponse CreateResponse(this PriceRule priceRule)
    {
        return new PriceRuleResponse
        {
            Id = priceRule.Id,
            BillboardSurface = priceRule.BillboardSurface!.CreateResponse(),
            BillboardType = priceRule.BillboardTypeId.ToString(),
            Price = priceRule.Price
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
            Status = manager.StatusId.ToString()
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
            StartTime = tariff.StartTime.ToString(FormatConstants.ValidTimeFormat),
            EndTime = tariff.EndTime.ToString(FormatConstants.ValidTimeFormat),
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

    public static BillboardSurfaceResponse CreateResponse(this BillboardSurface billboardSurface)
    {
        return new BillboardSurfaceResponse
        {
            Id = billboardSurface.Id,
            Surface = billboardSurface.Surface
        };
    }

    public static DiscountResponse CreateResponse(this Discount discount)
    {
        return new DiscountResponse
        {
            Id = discount.Id,
            Name = discount.Name,
            SalesOf = discount.SalesOf * 100m,
            MinRentCount = discount.MinRentCount,
            EndDate = discount.EndDate.ToLocalTime().ToString(FormatConstants.ValidDateFormat),
            Billboards = discount.Billboards.Select(e=>e.CreateShortResponse()).ToList()
        };
    }

    public static OrderResponse CreateResponse(this Order order)
    {
        return new OrderResponse
        {
            Id = order.Id,
            Billboard = order.Billboard.CreateResponse(),
            Tariff = order.SelectedTariff.CreateResponse(),
            StartDate = order.StartDate,
            EndDate = order.EndDate,
            RentPrice = order.RentPrice,
            ProductPrice = order.ProductPrice,
            PenaltyPrice = order.PenaltyPrice,
            User = order.User.CreateResponse()
        };
    }

    public static ShortBillboardResponse CreateShortResponse(this Billboard billboard)
    {
        return new ShortBillboardResponse
        {
            Id = billboard.Id,
            Name = billboard.Name
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

    public static AddTariff CreateAddTariff(this AddTariffRequest request)
    {
        return new AddTariff
        {
            Title = request.Title,
            StartTime = TimeSpan.Parse(request.StartTime),
            EndTime = TimeSpan.Parse(request.EndTime),
            Price = request.Price
        };
    }

    public static AddOrder CreateAddOrder(this AddOrderRequest request, Guid userId)
    {
        return new AddOrder
        {
            BillboardId = request.BillboardId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TariffId = request.TariffId,
            Files = request.Files,
            UserId = userId
        };
    }
}