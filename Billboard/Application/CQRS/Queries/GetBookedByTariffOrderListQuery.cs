using Application.InternalModels;
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
            var dateRanges = await _context.Orders
                .Include(e => e.SelectedTariff)
                .Where(e => e.SelectedTariff.Id == request.Request.TariffId)
                .ToListAsync(cancellationToken);
            return dateRanges
                .Select(e => new BookedOrderResponse
                {
                    OrderId = e.Id,
                    BookedDates = new DateRange
                    {
                        StartDate = e.StartDate,
                        EndDate = e.EndDate
                    },
                });
        }
    }
}