using Contracts.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;

namespace Application.CQRS.Commands;

public class DeleteGroupOfTariffsCommand : IRequest
{
    public required Guid GroupId { get; init; }
    
    public class DeleteGroupOfTariffsCommandHandler : IRequestHandler<DeleteGroupOfTariffsCommand>
    {
        public readonly BillboardContext _context;

        public DeleteGroupOfTariffsCommandHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteGroupOfTariffsCommand request, CancellationToken cancellationToken)
        {
            var groupOfTariffs =
                await _context.GroupOfTariffs.FirstOrDefaultAsync(e => e.Id == request.GroupId, cancellationToken);
            
            if (groupOfTariffs is null)
            {
                throw new NotFoundException($"Group of tariffs with id: {request.GroupId} not found");
            }

            groupOfTariffs.ArchiveStatusId = ArchiveStatusId.Archived;
            _context.GroupOfTariffs.Update(groupOfTariffs);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}