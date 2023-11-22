using Application.Extensions;
using Contracts.Exceptions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;

namespace Application.CQRS.Queries;

public class GetDiscountQuery : IRequest<DiscountResponse>
{
    public required Guid Id { get; set; }

    public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, DiscountResponse>
    {
        private readonly BillboardContext _context;

        public GetDiscountQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<DiscountResponse> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var discount = await _context.Discounts.FirstOrDefaultAsync(
                e => e.Id == request.Id && e.ArchiveStatusId == ArchiveStatusId.NonArchived, cancellationToken);
            if (discount is null)
            {
                throw new NotFoundException($"Discount with id: {request.Id} not found");
            }

            return discount.CreateResponse();
        }
    }
}