using Application.Extensions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetShortBillboardsQuery : IRequest<IEnumerable<ShortBillboardResponse>>
{
    public class GetShortBillboardsQueryHandler : IRequestHandler<GetShortBillboardsQuery, IEnumerable<ShortBillboardResponse>>
    {
        private readonly BillboardContext _context;

        public GetShortBillboardsQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShortBillboardResponse>> Handle(GetShortBillboardsQuery request, CancellationToken cancellationToken)
        {
            var billboards = await _context.Billboards.ToListAsync(cancellationToken);
            return billboards.Select(e => e.CreateShortResponse());
        }
    }
}