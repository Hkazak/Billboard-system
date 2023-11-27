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

public class AddPriceRuleCommand : IRequest<PriceRuleResponse>
{
    public required AddPriceRuleRequest Request { get; init; }

    public class AddPriceRuleCommandHandler : IRequestHandler<AddPriceRuleCommand, PriceRuleResponse>
    {
        private readonly BillboardContext _context;

        public AddPriceRuleCommandHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<PriceRuleResponse> Handle(AddPriceRuleCommand request, CancellationToken cancellationToken)
        {
            var billboardSurface = await _context.BillboardSurfaces
                .FirstOrDefaultAsync(e => e.Id == request.Request.BillboardSurfaceId, cancellationToken);
            if (billboardSurface is null)
            {
                throw new NotFoundException($"Billboard surface with id {request.Request.BillboardSurfaceId} not found");
            }

            var rule = new PriceRule
            {
                BillboardSurface = billboardSurface,
                BillboardSurfaceId = request.Request.BillboardSurfaceId,
                BillboardTypeId = Enum.Parse<BillboardTypeId>(request.Request.BillboardType),
                Price = request.Request.Price,
            };
            await _context.AddAsync(rule, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return rule.CreateResponse();
        }
    }
}