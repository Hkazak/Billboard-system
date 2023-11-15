using Application.Extensions;
using Contracts.Exceptions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetBillboardInformationQuery : IRequest<BillboardResponse>
{
    public required Guid BillboardId { get; init; }
    
    public class GetBillboardInformationQueryHandler : IRequestHandler<GetBillboardInformationQuery, BillboardResponse>
    {
        
        private readonly BillboardContext _context;

        public GetBillboardInformationQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<BillboardResponse> Handle(GetBillboardInformationQuery request,
            CancellationToken cancellationToken)
        {
            var billboard =
                await _context.Billboards.FirstOrDefaultAsync(e => e.Id == request.BillboardId, cancellationToken);
            if (billboard is null)
            {
                throw new NotFoundException($"Billboard with id: {request.BillboardId} not found");
            }

            return billboard.CreateResponse();
        }
    }
}