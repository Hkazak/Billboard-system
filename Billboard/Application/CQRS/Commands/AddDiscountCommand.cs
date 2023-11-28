using Application.Extensions;
using Contracts.Constants;
using Contracts.Requests;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;
using Persistence.Models;

namespace Application.CQRS.Commands;

public class AddDiscountCommand : IRequest<DiscountResponse>
{
    public required AddDiscountRequest Request { get; init; }
    
    public class AddDiscountCommandHandler : IRequestHandler<AddDiscountCommand, DiscountResponse>
    {
        private readonly BillboardContext _context;

        public AddDiscountCommandHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<DiscountResponse> Handle(AddDiscountCommand request, CancellationToken cancellationToken)
        {
            var billboards = await _context.Billboards
                .Where(e => request.Request.BillboardIds.Any(id => id == e.Id))
                .ToListAsync(cancellationToken);
            var discount = new Discount
            {
                Name = request.Request.Name,
                SalesOf = request.Request.DiscountPercentage / 100m,
                MinRentCount = request.Request.MinRentCount,
                EndDate = DateTime.ParseExact(request.Request.EndDate, FormatConstants.ValidDateFormat, null).ToUniversalTime(),
                ArchiveStatusId = ArchiveStatusId.NonArchived,
                Billboards = billboards
            };
            await _context.Discounts.AddAsync(discount, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return discount.CreateResponse();
        }
    }
}