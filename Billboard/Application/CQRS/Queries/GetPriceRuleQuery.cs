using Application.Extensions;
using Contracts.Exceptions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetPriceRuleQuery : IRequest<PriceRuleResponse>
{
    public required Guid PriceRuleId { get;init; }
    
    public class GetPriceRuleQueryHandler : IRequestHandler<GetPriceRuleQuery, PriceRuleResponse>
    {
        private readonly BillboardContext _context;

        public GetPriceRuleQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<PriceRuleResponse> Handle(GetPriceRuleQuery request, CancellationToken cancellationToken)
        {
            var priceRule = await _context.PriceRules
                .Include(e => e.BillboardSurface)
                .FirstOrDefaultAsync(e => e.Id == request.PriceRuleId, cancellationToken);
            if (priceRule is null)
            {
                throw new NotFoundException($"Price rule with id {request.PriceRuleId} not found");
            }

            return priceRule.CreateResponse();
        }
    }
}