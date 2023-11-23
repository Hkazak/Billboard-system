using Application.Extensions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetGroupOfTariffsListQuery : IRequest<IEnumerable<GroupOfTariffsResponse>>
{
    public class GerGroupOfTariffsListQueryHandler : IRequestHandler<GetGroupOfTariffsListQuery, IEnumerable<GroupOfTariffsResponse>>
    {
        private readonly BillboardContext _context;

        public GerGroupOfTariffsListQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GroupOfTariffsResponse>> Handle(GetGroupOfTariffsListQuery request, CancellationToken cancellationToken)
        {
            var groupOfTariffs = await _context.GroupOfTariffs
                .Include(e=>e.Tariffs)
                .ToListAsync(cancellationToken);
            return groupOfTariffs.Select(e => e.CreateResponse());
        }
    }
}