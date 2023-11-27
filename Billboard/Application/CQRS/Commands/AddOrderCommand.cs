using Application.Extensions;
using Contracts.DataTransferObjects;
using Contracts.Exceptions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Models;

namespace Application.CQRS.Commands;

public class AddOrderCommand : IRequest<OrderResponse>
{
    public required AddOrder Request { get; init; }

    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, OrderResponse>
    {
        private readonly BillboardContext _context;

        public AddOrderCommandHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<OrderResponse> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var billboard = await _context.Billboards
                .Include(e=>e.BillboardSurface)
                .FirstOrDefaultAsync(e => e.Id == request.Request.BillboardId, cancellationToken);
            var tariff = await _context.Tariffs
                .FirstOrDefaultAsync(e => e.Id == request.Request.TariffId, cancellationToken);
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Id == request.Request.UserId, cancellationToken);
            if (billboard is null)
            {
                throw new NotFoundException($"Billboard with id {request.Request.BillboardId} not found");
            }

            if (tariff is null)
            {
                throw new NotFoundException($"Tariff with id {request.Request.TariffId} not found");
            }
            
            if (user is null)
            {
                throw new NotFoundException($"User with id {request.Request.UserId} not found");
            }

            var priceRule = await _context.PriceRules
                .FirstOrDefaultAsync(
                    e => e.BillboardTypeId == billboard.TypeId && e.BillboardSurfaceId == billboard.BillboardSurface.Id,
                    cancellationToken);
            if (priceRule is null)
            {
                throw new NotFoundException($"Not found rule for surface {billboard.BillboardSurface.Id} billboard type {billboard.TypeId}");
            }

            var order = new Order
            {
                RentPrice = tariff.Price * (decimal)(request.Request.EndDate - request.Request.StartDate).TotalDays,
                PenaltyPrice = 0,
                ProductPrice = billboard.Width * billboard.Height * priceRule.Price,
                Billboard = billboard,
                StartDate = request.Request.StartDate,
                EndDate = request.Request.EndDate,
                User = user,
                SelectedTariff = tariff
            };
            await _context.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return order.CreateResponse();
        }
    }
}