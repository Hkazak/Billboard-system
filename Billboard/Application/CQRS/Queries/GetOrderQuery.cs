using Application.Extensions;
using Contracts.Exceptions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;

namespace Application.CQRS.Queries;

public class GetOrderQuery : IRequest<OrderResponse>
{
    public required Guid OrderId { get; init; }
    public required Guid RequestSenderId { get; init; }
    
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderResponse>
    {
        private readonly BillboardContext _context;

        public GetOrderQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<OrderResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var isUser = await _context.Users
                .AnyAsync(e => e.Id == request.RequestSenderId && e.RoleId == UserRoleId.Client, cancellationToken);
            var prepareOrders = _context.Orders.AsQueryable();
            if (isUser)
            {
                prepareOrders = prepareOrders.Where(e => e.UserId == request.RequestSenderId);
            }

            var order = await prepareOrders
                .Include(e => e.Billboard!)
                .ThenInclude(e => e.BillboardSurface)
                .Include(e => e.SelectedTariff)
                .Include(e => e.User!)
                .Include(e => e.Pictures)
                .FirstOrDefaultAsync(e => e.Id == request.OrderId, cancellationToken);
            if (order is null)
            {
                throw new NotFoundException($"Order with id {request.OrderId} not found");
            }

            return order.CreateResponse();
        }
    }
}