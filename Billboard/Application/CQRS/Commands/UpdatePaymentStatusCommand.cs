using Application.Extensions;
using Application.Services;
using Contracts.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;

namespace Application.CQRS.Commands;

public class UpdatePaymentStatusCommand : IRequest
{
    public required Guid OrderId { get; init; }
    
    public class UpdatePaymentStatusCommandHandler : IRequestHandler<UpdatePaymentStatusCommand>
    {
        private readonly BillboardContext _context;
        private readonly IPaymentService _paymentService;

        public UpdatePaymentStatusCommandHandler(BillboardContext context, IPaymentService paymentService)
        {
            _context = context;
            _paymentService = paymentService;
        }

        public async Task Handle(UpdatePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(
                e => e.OrderId == request.OrderId && e.PaymentStatusId == PaymentStatusId.Unpaid &&
                     e.DueDate > DateTime.UtcNow, cancellationToken);
            if (payment is null)
            {
                throw new NotFoundException($"Didn't find any unpaid payment to order {request.OrderId}");
            }

            var order = await _paymentService.GetOrderById(payment.ExternalOrderId, cancellationToken);
            payment.PaymentStatusId = order.Status.ConvertToEnum();
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}