﻿using Contracts.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;

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
            var order = await _context.Orders
                .FirstOrDefaultAsync(e => e.Id == request.OrderId, cancellationToken);
            if (order is null)
            {
                throw new NotFoundException($"Order with id {request.OrderId} not found");
            }

            order.StatusId = OrderStatusId.InProgress;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}