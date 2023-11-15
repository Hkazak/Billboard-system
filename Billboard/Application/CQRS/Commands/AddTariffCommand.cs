using Application.Extensions;
using Contracts.Requests;
using Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using Persistence.Context;
using Persistence.Enums;
using Persistence.Models;

namespace Application.CQRS.Commands;

public class AddTariffCommand : IRequest<TariffResponse>
{
    public required AddTariffRequest Request { get; init; }

    public class AddTariffCommandHandler : IRequestHandler<AddTariffCommand, TariffResponse>
    {
        private readonly BillboardContext _context;

        public AddTariffCommandHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<TariffResponse> Handle(AddTariffCommand request, CancellationToken cancellationToken)
        {
            var tariff = new Tariff
            {
                Title = request.Request.Title,
                StartTime = request.Request.StartTime,
                EndTime = request.Request.EndTime,
                Price = request.Request.Price,
                ArchiveStatusId = ArchiveStatusId.NonArchived
            };

            await _context.Tariffs.AddAsync(tariff, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return tariff.CreateResponse();
        }
    }
}