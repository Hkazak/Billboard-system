using System.Globalization;
using Application.Extensions;
using Application.Services;
using Contracts.Constants;
using Contracts.Exceptions;
using Contracts.Requests;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Models;

namespace Application.CQRS.Commands;

public class CreatePaymentCommand : IRequest<PaymentResponse>
{
    public required CreatePaymentRequest Request { get; init; }

    public class CreatePaymentForOrderCommandHandler : IRequestHandler<CreatePaymentCommand, PaymentResponse>
    {
        private readonly BillboardContext _context;
        private readonly IPaymentService _paymentService;

        public CreatePaymentForOrderCommandHandler(BillboardContext context, IPaymentService paymentService)
        {
            _context = context;
            _paymentService = paymentService;
        }

        public async Task<PaymentResponse> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(e => e.Id == request.Request.OrderId, cancellationToken);
            if (order is null)
            {
                throw new NotFoundException($"Order with id {request.Request.OrderId} not found");
            }

            var backUrl = $"{_paymentService.BackUrl}/{order.Id}";
            var successUrl = $"{_paymentService.SuccessUrl}/{order.Id}";
            var failureUrl = $"{_paymentService.FailureUrl}/{order.Id}";
            var paymentRequest = order.CreatePaymentOrderRequest(backUrl, successUrl, failureUrl);
            var response = await _paymentService.CreateOrderAsync(paymentRequest, cancellationToken);
            var utcDueDate = response.Order.DueDate.ToDateIoka().ToLocalTime().ToUniversalTime();
            var payment = new Payment
            {
                OrderId = request.Request.OrderId,
                ExternalOrderId = response.Order.Id,
                PaymentStatusId = response.Order.Status.ConvertToEnum(),
                DueDate = utcDueDate
            };
            await _context.Payments.AddAsync(payment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return response.CreateResponse();
        }
    }
}