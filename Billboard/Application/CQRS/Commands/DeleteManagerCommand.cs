using Contracts.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;

namespace Application.CQRS.Commands;

public class DeleteManagerCommand : IRequest
{
    public required Guid ManagerId { get; init; }

    public class DeleteManagerCommandHandler : IRequestHandler<DeleteManagerCommand>
    {
        private readonly BillboardContext _context;

        public DeleteManagerCommandHandler(BillboardContext context)
        {
            _context = context;
        }


        public async Task Handle(DeleteManagerCommand request, CancellationToken cancellationToken)
        {
            var manager = await _context.Managers.FirstOrDefaultAsync(x => x.Id == request.ManagerId && x.StatusId == ManagerStatusId.Active, cancellationToken);
            if (manager is null)
            {
                throw new NotFoundException($"Manager with this id {request.ManagerId} not found");
            }

            manager.StatusId = ManagerStatusId.Inactive;
            _context.Managers.Update(manager);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}