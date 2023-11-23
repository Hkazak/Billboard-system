using Application.Extensions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetBillboardsInformationQuery : IRequest<IEnumerable<BillboardResponse>>
{
    public class GetBillboardsInformationQueryHandler : IRequestHandler<GetBillboardsInformationQuery, IEnumerable<BillboardResponse>>
    {
        private readonly BillboardContext _context;

        public GetBillboardsInformationQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BillboardResponse>> Handle(GetBillboardsInformationQuery request,
            CancellationToken cancellationToken)
        {
            var billboards = await _context.Billboards.Include(e => e.GroupOfTariffs).ThenInclude(e => e.Tariffs).Include(e => e.BillboardSurface).Include(e => e.BillboardType).Include(e => e.Pictures).Include(e => e.ArchiveStatus).ToListAsync(cancellationToken);
            return billboards.Select(e => e.CreateResponse());
        }
    }
}