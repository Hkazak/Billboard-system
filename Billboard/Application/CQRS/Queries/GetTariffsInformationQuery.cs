using System.Reflection.Metadata;
using Application.Extensions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

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
            var tariffs = await _context.Tariffs.ToListAsync(cancellationToken);
            return tariffs.Select(e => e.CreateResponse());
        }
    }
}