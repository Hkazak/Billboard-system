using Contracts.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;

namespace Application.CQRS.Commands;

public class ActivateManagerCommand : IRequest
{
    public required Guid ManagerId { get; init; }

    public class ActivateManagerCommandHandler : IRequestHandler<ActivateManagerCommand>
    {
        private readonly BillboardContext _context;

        public ActivateManagerCommandHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task Handle(ActivateManagerCommand request, CancellationToken cancellationToken)
        {
            var manager = await _context.Managers.FirstOrDefaultAsync(e => e.Id == request.ManagerId && e.StatusId == ManagerStatusId.Inactive, cancellationToken);
            if (manager is null)
            {
                throw new NotFoundException($"Manager with this id {request.ManagerId} not found");
            }

            manager.StatusId = ManagerStatusId.Active;
            _context.Managers.Update(manager);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}