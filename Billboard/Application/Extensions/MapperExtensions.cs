using System.Globalization;
using Application.InternalModels;
using Application.InternalModels.PaymentService.Models;
using Application.InternalModels.PaymentService.Requests;
using Application.InternalModels.PaymentService.Responses;
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
        var pictures = billboard.Pictures
            .Where(e => File.Exists(e.Source))
            .Select(e => $"/pictures/{Path.GetFileName(e.Source)}")
            .ToList();
        if (pictures.Count == 0)
        {
            pictures.Add("/pictures/no-image.png");
        }

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
            PictureSource = pictures,
            GroupOfTariffs = billboard.GroupOfTariffs.CreateResponse(),
            Discounts = billboard.Discounts.Select(e => e.CreateResponse()).ToList()
        };
    }

    public static CreateOrderRequest CreatePaymentOrderRequest(this Order order, string backUrl, string successUrl, string failureUrl)
    {
        return new CreateOrderRequest
        {
            Amount = Math.Min(int.MaxValue, (long)((order.ProductPrice + order.PenaltyPrice + order.RentPrice) * 100)),
            Currency = "KZT",
            CaptureMethod = "AUTO",
            ExternalId = order.Id.ToString(),
            Description = $"Order final price: product price {order.ProductPrice} + {order.RentPrice} + {order.PenaltyPrice}",
            Attempts = 10,
            DueDate = DateTime.UtcNow.AddHours(1).ToString(FormatConstants.ValidIokaDateTimeFormat),
            BackUrl = backUrl.Contains("localhost") ? "http://example.com" : backUrl,
            SuccessUrl = successUrl.Contains("localhost") ? "http://example.com" : successUrl,
            FailureUrl = failureUrl.Contains("localhost") ? "http://example.com" : failureUrl,
        };
    }

    public static PaymentResponse CreateResponse(this CreateOrderResponse response)
    {
        return new PaymentResponse
        {
            Id = Guid.Parse(response.Order.ExternalId),
            CheckoutUrl = response.Order.CheckoutUrl
        };
    }

    public static PaymentResponse CreateResponse(this PaymentOrder order)
    {
        return new PaymentResponse
        {
            Id = Guid.Parse(order.ExternalId),
            CheckoutUrl = order.CheckoutUrl
        };
    }

    public static OrderResponse CreateResponse(this Order order)
    {
        var pictures = order.Pictures
            .Where(e => File.Exists(e.Source))
            .Select(e => $"/pictures/{Path.GetFileName(e.Source)}")
            .ToList();
        var billboardPictures = order.Billboard!.Pictures
            .Where(e => File.Exists(e.Source))
            .Select(e => $"/pictures/{Path.GetFileName(e.Source)}")
            .ToList();
        if (pictures.Count == 0)
        {
            pictures.Add("/pictures/no-image.png");
        }

        if (billboardPictures.Count == 0)
        {
            billboardPictures.Add("/pictures/no-image.png");
        }

        return new OrderResponse
        {
            Id = order.Id,
            Name = order.Billboard!.Name,
            Description = order.Billboard.Description,
            BillboardType = order.Billboard.TypeId.ToString(),
            BillboardSurface = order.Billboard.BillboardSurface.Surface,
            Width = order.Billboard.Width,
            Height = order.Billboard.Height,
            PenaltyPrice = order.PenaltyPrice,
            Tariff = order.SelectedTariff!.CreateResponse(),
            StartDate = order.StartDate.ToLocalTime()
                .ToString(FormatConstants.ValidDateFormat),
            EndDate = order.EndDate.ToLocalTime()
                .ToString(FormatConstants.ValidDateFormat),
            RentPrice = order.RentPrice,
            ProductPrice = order.ProductPrice,
            Discount = order.Discount?.CreateResponse(),
            UploadedFiles = pictures,
            Status = order.StatusId.ToString(),
            UserName = order.User!.Name,
            UserEmail = order.User!.Email,
            BillboardDescription = order.Billboard.Description,
            BillboardPictures = billboardPictures
        };
    }

    public static BookedOrderResponse CreateBookedResponse(this Order order)
    {
        return new BookedOrderResponse
        {
            OrderId = order.Id,
            StartDate = order.StartDate.ToLocalTime().ToString(FormatConstants.ValidDateFormat),
            EndDate = order.EndDate.ToLocalTime().ToString(FormatConstants.ValidDateFormat)
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
            StartDate = DateTime.ParseExact(request.StartDate, FormatConstants.ValidDateFormat, null, DateTimeStyles.None),
            EndDate = DateTime.ParseExact(request.EndDate, FormatConstants.ValidDateFormat, null, DateTimeStyles.None),
            TariffId = request.TariffId,
            Files = request.Files,
            UserId = userId
        };
    }
}