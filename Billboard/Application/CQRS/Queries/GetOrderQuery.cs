using Application.Extensions;
using Contracts.Exceptions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetOrderQuery : IRequest<OrderResponse>
{
    public required Guid OrderId { get; init; }
    
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderResponse>
    {
        private readonly BillboardContext _context;

        public GetOrderQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<OrderResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(e => e.Billboard)
                .Include(e => e.SelectedTariff)
                .FirstOrDefaultAsync(e => e.Id == request.OrderId, cancellationToken);
            if (order is null)
            {
                throw new NotFoundException($"Order with id {request.OrderId} not found");
            }

            return order.CreateResponse();
        }
    }
}