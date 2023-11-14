using Contracts.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;

namespace Application.CQRS.Commands;

public class DeleteBillboardCommand : IRequest
{
    public required Guid BillboardId { get; init; }
    
    public class DeleteBillboardCommandHandler : IRequestHandler<DeleteBillboardCommand>
    {
        private readonly BillboardContext _context;

        public DeleteBillboardCommandHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteBillboardCommand request, CancellationToken cancellationToken)
        {
            var billboard =
                await _context.Billboards.FirstOrDefaultAsync(e => e.Id == request.BillboardId, cancellationToken);
            if (billboard is null)
            {
                throw new NotFoundException($"Billboard with id: {request.BillboardId} not found");
            }

            billboard.ArchiveStatusId = ArchiveStatusId.Archived;
            _context.Billboards.Update(billboard);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}