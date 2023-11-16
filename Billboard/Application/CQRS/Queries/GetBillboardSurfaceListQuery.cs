using Application.Extensions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Models;

namespace Application.CQRS.Queries;

public class GetBillboardSurfaceListQuery : IRequest<IEnumerable<BillboardSurfaceResponse>>
{
    public class GetBillboardSurfaceListQueryHandler : IRequestHandler<GetBillboardSurfaceListQuery, IEnumerable<BillboardSurfaceResponse>>
    {
        private readonly BillboardContext _context;

        public GetBillboardSurfaceListQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BillboardSurfaceResponse>> Handle(GetBillboardSurfaceListQuery request,
            CancellationToken cancellationToken)
        {
            var billboardsSurfaceList = await _context.BillboardSurfaces.ToListAsync(cancellationToken);
            return billboardsSurfaceList.Select(e => e.CreateResponse());
        }
    }
}