using Application.InternalModels.PaymentService.Requests;
using Application.Services;
using Contracts.Constants;
using Contracts.DataTransferObjects;
using Contracts.Exceptions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;

namespace Application.CQRS.Commands;

public class ChangeOrderCommand : IRequest<PaymentResponse>
{
    public required ChangeOrder Request { get; init; }

    public class ChangedOrderCommandHandler : IRequestHandler<ChangeOrderCommand, PaymentResponse>
    {
        private readonly BillboardContext _context;
        private readonly IPaymentService _paymentService;

        public ChangedOrderCommandHandler(BillboardContext context, IPaymentService paymentService)
        {
            _context = context;
            _paymentService = paymentService;
        }

        public async Task<PaymentResponse> Handle(ChangeOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(e => e.Billboard)
                .Include(e => e.SelectedTariff)
                .FirstOrDefaultAsync(e => e.Id == request.Request.OrderId, cancellationToken);
            if (order is null)
            {
                throw new NotFoundException($"Order with id {request.Request.OrderId} not found");
            }

            if (order.UserId != request.Request.RequestSenderId)
            {
                throw new NotPermissionsException(
                    $"User with id {request.Request.RequestSenderId} don't have permissions to modify order {request.Request.OrderId}");
            }

            var days = (decimal)(request.Request.EndDate - request.Request.StartDate).TotalDays;
            order.PenaltyPrice = order.Billboard!.Penalty;
            order.RentPrice = days * order.SelectedTariff!.Price;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken);
            var payment = await _context.Payments.FirstOrDefaultAsync(
                e => e.OrderId == request.Request.OrderId && e.PaymentStatusId == PaymentStatusId.Unpaid,
                cancellationToken);
            if (payment is null)
            {
                throw new InternalServiceException($"Payment related with order {order.Id} not found");
            }

            var amount = (order.PenaltyPrice + order.RentPrice + order.ProductPrice) * 100;
            var body = new CreateOrderRequest
            {
                Amount = Math.Min((long)amount, int.MaxValue),
                Currency = "KZT",
                CaptureMethod = "AUTO",
                ExternalId = payment.ExternalOrderId,
                Description = $"Additional payment by id {payment.OrderId}",
                Attempts = 10,
                DueDate = DateTime.UtcNow.AddDays(7).ToString(FormatConstants.ValidIokaDateTimeFormat),
                BackUrl = _paymentService.BackUrl.Contains("localhost") ? "http://example.com" : _paymentService.BackUrl,
                SuccessUrl = _paymentService.SuccessUrl.Contains("localhost") ? "http://example.com" : _paymentService.SuccessUrl,
                FailureUrl = _paymentService.FailureUrl.Contains("localhost") ? "http://example.com" : _paymentService.FailureUrl
            };
            var response = await _paymentService.CreateOrderAsync(body, cancellationToken);
            return new PaymentResponse
            {
                Id = payment.Id,
                CheckoutUrl = response.Order.CheckoutUrl
            };
        }
    }
}