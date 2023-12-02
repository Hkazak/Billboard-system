using Application.Extensions;
using Contracts.Requests;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetBookedByTariffOrderListQuery : IRequest<IEnumerable<BookedOrderResponse>>
{
    public required BookedByTariffRequest Request { get; init; }

    public class GetBookedByTariffOrderListQueryHandler : IRequestHandler<GetBookedByTariffOrderListQuery, IEnumerable<BookedOrderResponse>>
    {
        private readonly BillboardContext _context;

        public GetBookedByTariffOrderListQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookedOrderResponse>> Handle(GetBookedByTariffOrderListQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .Include(e => e.SelectedTariff)
                .Where(e => request.Request.BillboardId == e.BillboardId && (request.Request.TariffId == Guid.Empty || e.SelectedTariff!.Id == request.Request.TariffId))
                .ToListAsync(cancellationToken);
            return orders.Select(e => e.CreateBookedResponse());
        }
    }
}