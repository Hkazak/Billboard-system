using Application.InternalModels.PaymentService.Models;
using Application.InternalModels.PaymentService.Requests;
using Application.InternalModels.PaymentService.Responses;

namespace Application.Services;

public interface IPaymentService
{
    string BackUrl { get; }
    string SuccessUrl { get; }
    string FailureUrl { get; }
    Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default);
    Task<PaymentOrder> GetOrderById(string externalOrderId, CancellationToken cancellationToken = default);
}