using System.Globalization;
using Contracts.Constants;
using Contracts.Exceptions;
using Contracts.Requests;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class CalculateOrderPriceQuery : IRequest<OrderPriceResponse>
{
    public required CalculateOrderPriceRequest Request { get; init; }

    public class CalculateOrderPriceQueryHandler : IRequestHandler<CalculateOrderPriceQuery, OrderPriceResponse>
    {
        private readonly BillboardContext _context;

        public CalculateOrderPriceQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<OrderPriceResponse> Handle(CalculateOrderPriceQuery request, CancellationToken cancellationToken)
        {
            var billboard = await _context.Billboards
                .Include(e=>e.BillboardSurface)
                .FirstOrDefaultAsync(e => e.Id == request.Request.BillboardId, cancellationToken);
            var tariff = await _context.Tariffs
                .FirstOrDefaultAsync(e => e.Id == request.Request.TariffId, cancellationToken);
            if (billboard is null)
            {
                throw new NotFoundException($"Billboard with id {request.Request.BillboardId} not found");
            }

            if (tariff is null)
            {
                throw new NotFoundException($"Tariff with id {request.Request.TariffId} not found");
            }

            var priceRule = await _context.PriceRules
                .FirstOrDefaultAsync(
                    e => e.BillboardTypeId == billboard.TypeId && e.BillboardSurfaceId == billboard.BillboardSurface.Id,
                    cancellationToken);
            if (priceRule is null)
            {
                throw new NotFoundException($"Not found rule for surface {billboard.BillboardSurface.Id} billboard type {billboard.TypeId}");
            }

            var startDate = DateTime.ParseExact(request.Request.StartDate, FormatConstants.ValidDateFormat, null, DateTimeStyles.None);
            var endDate = DateTime.ParseExact(request.Request.EndDate, FormatConstants.ValidDateFormat, null, DateTimeStyles.None);
            var days = (decimal)(endDate - startDate).TotalDays;
            return new OrderPriceResponse
            {
                RentPrice = tariff.Price * days,
                PenaltyPrice = 0,
                ProductPrice = billboard.Width * billboard.Height * priceRule.Price,
            };
        }
    }
}