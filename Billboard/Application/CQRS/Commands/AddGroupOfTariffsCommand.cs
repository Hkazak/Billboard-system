using System.Reflection.Metadata;
using Application.Extensions;
using Contracts.Exceptions;
using Contracts.Requests;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;
using Persistence.Models;

namespace Application.CQRS.Commands;

public class AddGroupOfTariffsCommand : IRequest<GroupOfTariffsResponse>
{
    public required AddGroupOfTariffsRequest Request { get; init; }

    public class AddGroupOfTariffsCommandHandler : IRequestHandler<AddGroupOfTariffsCommand, GroupOfTariffsResponse>
    {
        private readonly BillboardContext _context;

        public AddGroupOfTariffsCommandHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<GroupOfTariffsResponse> Handle(AddGroupOfTariffsCommand request,
            CancellationToken cancellationToken)
        {
            var tariffs = await _context.Tariffs
                    .Where(e => request.Request.TariffsId.Any(x => x == e.Id))
                    .ToListAsync(cancellationToken);
            if (tariffs.Select(e => e.EndTime - e.StartTime).Sum(x => x.TotalDays) > 1)
            {
                throw new InvalidRequestDataException("Tariffs total time must be less than 24 hours");
            }
            
            var groupOfTariffs = new GroupOfTariffs
            {
                Name = request.Request.Name,
                Tariffs = tariffs,
                ArchiveStatusId = ArchiveStatusId.NonArchived
            };

            await _context.GroupOfTariffs.AddAsync(groupOfTariffs, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return groupOfTariffs.CreateResponse();
        }
    }
}