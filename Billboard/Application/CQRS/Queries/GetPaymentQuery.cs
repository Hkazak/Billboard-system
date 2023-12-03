using Application.Extensions;
using Application.Services;
using Contracts.Exceptions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetPaymentQuery : IRequest<PaymentResponse>
{
    public required Guid PaymentId { get; init; }

    public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, PaymentResponse>
    {
        private readonly BillboardContext _context;
        private readonly IPaymentService _paymentService;

        public GetPaymentQueryHandler(BillboardContext context, IPaymentService paymentService)
        {
            _context = context;
            _paymentService = paymentService;
        }

        public async Task<PaymentResponse> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(e => e.Id == request.PaymentId, cancellationToken);
            if (payment is null)
            {
                throw new NotFoundException($"Payment with id {request.PaymentId} not found");
            }

            var response = await _paymentService.GetOrderById(payment.ExternalOrderId, cancellationToken);
            return response.CreateResponse();
        }
    }
}