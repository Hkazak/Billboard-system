using Application.Extensions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetPriceRuleListQuery : IRequest<IEnumerable<PriceRuleResponse>>
{
    public class GetPriceRuleListQueryHandler : IRequestHandler<GetPriceRuleListQuery, IEnumerable<PriceRuleResponse>>
    {
        private readonly BillboardContext _context;

        public GetPriceRuleListQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PriceRuleResponse>> Handle(GetPriceRuleListQuery request, CancellationToken cancellationToken)
        {
            var rules = await _context.PriceRules
                .Include(e => e.BillboardSurface)
                .ToListAsync(cancellationToken);
            return rules.Select(e => e.CreateResponse());
        }
    }
}