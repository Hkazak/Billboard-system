using Application.Extensions;
using Contracts.Constants;
using Contracts.Requests;
using Contracts.Responses;
using MediatR;
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
            var discount = new Discount
            {
                Name = request.Request.Name,
                SalesOf = request.Request.SalesOf,
                MinRentCount = request.Request.MinRentCount,
                EndDate = DateTime.ParseExact(request.Request.EndDate, ValidationConstants.ValidDateFormat, null).ToUniversalTime(),
                ArchiveStatusId = ArchiveStatusId.NonArchived
            };
            await _context.Discounts.AddAsync(discount, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return discount.CreateResponse();
        }
    }
}