using Application.Extensions;
using Contracts.Exceptions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetBillboardSurfaceQuery : IRequest<BillboardSurfaceResponse>
{
    public required Guid Id { get; init; }
    
    public class GetBillboardSurfaceQueryHandler : IRequestHandler<GetBillboardSurfaceQuery, BillboardSurfaceResponse>
    {
        private readonly BillboardContext _context;

        public GetBillboardSurfaceQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<BillboardSurfaceResponse> Handle(GetBillboardSurfaceQuery request, CancellationToken cancellationToken)
        {
            var billboardSurface =
                await _context.BillboardSurfaces.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (billboardSurface is null)
            {
                throw new NotFoundException($"Billboard surface with id: {request.Id} not found");
            }

            return billboardSurface.CreateResponse();
        }
    }
}