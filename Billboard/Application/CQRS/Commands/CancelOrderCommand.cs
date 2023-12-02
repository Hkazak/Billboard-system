using Contracts.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;

namespace Application.CQRS.Commands;

public class CancelOrderCommand : IRequest
{
    public required Guid OrderId { get; init; }
    public required Guid RequestSenderId { get; init; }

    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly BillboardContext _context;

        public CancelOrderCommandHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(e => e.Id == request.OrderId, cancellationToken);
            if (order is null)
            {
                throw new NotFoundException($"Order with id {request.OrderId} not found");
            }

            var isClient = _context.Users
                .AnyAsync(e => e.Id == request.RequestSenderId && e.RoleId == UserRoleId.Client, cancellationToken);
            if (order.UserId != request.RequestSenderId || await isClient)
            {
                throw new NotPermissionsException($"Client with id {request.RequestSenderId} don't have access to cancel order");
            }

            order.StatusId = OrderStatusId.Cancelled;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}