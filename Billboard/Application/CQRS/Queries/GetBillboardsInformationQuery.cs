﻿using Application.Extensions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;

namespace Application.CQRS.Queries;

public class GetBillboardsInformationQuery : IRequest<IEnumerable<BillboardResponse>>
{
    public class GetBillboardsInformationQueryHandler : IRequestHandler<GetBillboardsInformationQuery, IEnumerable<BillboardResponse>>
    {
        private readonly BillboardContext _context;

        public GetBillboardsInformationQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BillboardResponse>> Handle(GetBillboardsInformationQuery request, CancellationToken cancellationToken)
        {
            var billboards = await _context.Billboards
                .Where(e => e.ArchiveStatusId == ArchiveStatusId.NonArchived)
                .Include(e => e.Pictures)
                .Include(e => e.BillboardSurface)
                .Include(e => e.GroupOfTariffs)
                .ThenInclude(e => e.Tariffs)
                .Include(e => e.Discounts)
                .ToListAsync(cancellationToken);
            return billboards.Select(e => e.CreateResponse());
        }
    }
}