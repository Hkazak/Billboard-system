using System.Reflection.Metadata;
using Application.Extensions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using Persistence.Context;
using Persistence.Enums;

namespace Application.CQRS.Queries;

public class GetTariffsInformationQuery : IRequest<IEnumerable<TariffResponse>>
{
    public class GetTariffsInformationQueryHandler : IRequestHandler<GetTariffsInformationQuery, IEnumerable<TariffResponse>>
    {
        private readonly BillboardContext _context;

        public GetTariffsInformationQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TariffResponse>> Handle(GetTariffsInformationQuery request, CancellationToken cancellationToken)
        {
            var tariffs = await _context.Tariffs.Where(e => e.ArchiveStatusId == ArchiveStatusId.NonArchived).ToListAsync(cancellationToken);
            return tariffs.Select(e => e.CreateResponse());
        }
    }
}