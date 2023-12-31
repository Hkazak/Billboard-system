﻿using Application.Extensions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;

namespace Application.CQRS.Queries;

public class GetDiscountListQuery : IRequest<IEnumerable<DiscountResponse>>
{
    public class GetDiscountListQueryHandler : IRequestHandler<GetDiscountListQuery, IEnumerable<DiscountResponse>>
    {
        private readonly BillboardContext _context;

        public GetDiscountListQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DiscountResponse>> Handle(GetDiscountListQuery request,
            CancellationToken cancellationToken)
        {
            var discountList = await _context.Discounts
                .Include(e => e.Billboards.Where(b => b.ArchiveStatusId == ArchiveStatusId.NonArchived))
                .ToListAsync(cancellationToken);
            return discountList.Select(e => e.CreateResponse());
        }
    }
}