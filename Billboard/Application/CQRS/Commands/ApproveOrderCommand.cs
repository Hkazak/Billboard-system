using System.Data;
using Contracts.Constants;
using Contracts.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;
using Persistence.Extensions;

namespace Application.CQRS.Commands;

public class ApproveOrderCommand : IRequest
{
    public required Guid OrderId { get; init; }

    public class ApproveOrderCommandHandler : IRequestHandler<ApproveOrderCommand>
    {
        private readonly BillboardContext _context;

        public ApproveOrderCommandHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task Handle(ApproveOrderCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead, cancellationToken);
            try
            {
                var order = await _context.Orders.FirstOrDefaultAsync(e => e.Id == request.OrderId, cancellationToken);
                if (order is null)
                {
                    throw new NotFoundException($"Order with id {request.OrderId} not found");
                }

                var isNotIntersect = await _context.IsNotIntersectAsync(order.StartDate, order.EndDate, order.BillboardId, order.SelectedTariffId, cancellationToken);
                if (!isNotIntersect)
                {
                    throw new DataConflictException($"Order with id {order.Id} can't be approved, because some days from {order.StartDate.ToLocalTime().ToString(FormatConstants.ValidDateFormat)} to {order.EndDate.ToLocalTime().ToString(FormatConstants.ValidDateFormat)} already rent");
                }

                order.StatusId = OrderStatusId.InProgress;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}