using Contracts.DataTransferObjects;
using Contracts.Exceptions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class RecalculateOrderPriceQuery : IRequest<OrderPriceResponse>
{
    public required RecalculateOrderPrice Request { get; init; }
    
    public class RecalculateOrderPriceQueryHandler : IRequestHandler<RecalculateOrderPriceQuery, OrderPriceResponse>
    {
        private readonly BillboardContext _context;

        public RecalculateOrderPriceQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<OrderPriceResponse> Handle(RecalculateOrderPriceQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(e => e.Billboard!)
                .Include(e=>e.SelectedTariff!)
                .FirstOrDefaultAsync(e => e.Id == request.Request.OrderId, cancellationToken);
            if (order is null)
            {
                throw new NotFoundException($"Order with id {request.Request.OrderId} not found");
            }

            var days = (decimal)(request.Request.EndDate - request.Request.StartDate).TotalDays;
            return new OrderPriceResponse
            {
                ProductPrice = order.ProductPrice,
                RentPrice = days * order.SelectedTariff!.Price,
                PenaltyPrice = order.Billboard!.Penalty
            };
        }
    }
}