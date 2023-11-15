using Contracts.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;

namespace Application.CQRS.Commands;

public class DeleteTariffCommand : IRequest
{
    public required Guid TariffId { get; init; }

    public class DeleteTariffCommandHandler : IRequestHandler<DeleteTariffCommand>
    {
        private readonly BillboardContext _context;

        public DeleteTariffCommandHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteTariffCommand request, CancellationToken cancellationToken)
        {
            var tariff = await _context.Tariffs.FirstOrDefaultAsync(e => e.Id == request.TariffId, cancellationToken);
            if (tariff is null)
            {
                throw new NotFoundException($"Tariff with id: {request.TariffId} not found");
            }

            tariff.ArchiveStatusId = ArchiveStatusId.Archived;
            _context.Tariffs.Update(tariff);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}