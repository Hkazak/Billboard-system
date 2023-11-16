using Application.Extensions;
using Contracts.Requests;
using Contracts.Responses;
using MediatR;
using Persistence.Context;
using Persistence.Models;

namespace Application.CQRS.Commands;

public class AddBillboardSurfaceCommand : IRequest<Contracts.Responses.BillboardSurfaceResponse>
{
    public required AddBillboardSurfaceRequest Request { get; init; }

    public class AddBillboardSurfaceHandler : IRequestHandler<AddBillboardSurfaceCommand, Contracts.Responses.BillboardSurfaceResponse>
    {
        private readonly BillboardContext _context;

        public AddBillboardSurfaceHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<BillboardSurfaceResponse> Handle(AddBillboardSurfaceCommand request,
            CancellationToken cancellationToken)
        {
            var billboardSurface = new BillboardSurface
            {
                Surface = request.Request.Surface
            };
            await _context.BillboardSurfaces.AddAsync(billboardSurface, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return billboardSurface.CreateResponse();
        }
    }
}