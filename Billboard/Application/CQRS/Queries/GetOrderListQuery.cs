using Application.Extensions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;

namespace Application.CQRS.Queries;

public class GetOrderListQuery : IRequest<IEnumerable<OrderResponse>>
{
    public required Guid RequestSenderId { get; init; }

    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, IEnumerable<OrderResponse>>
    {
        private readonly BillboardContext _context;

        public GetOrderListQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderResponse>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var prepareOrders = _context.Orders.AsQueryable();
            var isUser = await _context.Users
                .AnyAsync(e => e.Id == request.RequestSenderId && e.RoleId == UserRoleId.Client, cancellationToken);
            if (isUser)
            {
                prepareOrders = prepareOrders.Where(e => e.UserId == request.RequestSenderId);
            }

            var orders = await prepareOrders
                .Include(e => e.Billboard!)
                .ThenInclude(e => e.BillboardSurface)
                .Include(e => e.User)
                .Include(e => e.SelectedTariff)
                .Include(e => e.Pictures)
                .ToListAsync(cancellationToken);
            return orders.Select(e => e.CreateResponse());
        }
    }
}