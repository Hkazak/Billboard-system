using Contracts.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;

namespace Application.CQRS.Commands;

public class DeleteDiscountCommand : IRequest
{
    public required Guid DiscountId;
    
    public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand>
    {
        private readonly BillboardContext _context;

        public DeleteDiscountCommandHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = await _context.Discounts
                .FirstOrDefaultAsync(
                    e => e.Id == request.DiscountId && e.ArchiveStatusId == ArchiveStatusId.NonArchived,
                    cancellationToken);
            if (discount is null)
            {
                throw new NotFoundException($"Discount with id: {request.DiscountId} not found");
            }

            discount.ArchiveStatusId = ArchiveStatusId.Archived;
            _context.Discounts.Update(discount);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}