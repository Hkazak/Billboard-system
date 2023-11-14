using Application.Extensions;
using Contracts.Exceptions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetTariffInformationQuery : IRequest<TariffResponse>
{
    public required Guid TariffId;
    
    public class GetTariffInformationQueryHandler : IRequestHandler<GetTariffInformationQuery, TariffResponse>
    {
        private readonly BillboardContext _context;

        public GetTariffInformationQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<TariffResponse> Handle(GetTariffInformationQuery request, CancellationToken cancellationToken)
        {
            var tariff = await _context.Tariffs.FirstOrDefaultAsync(e => e.Id == request.TariffId, cancellationToken);
            if (tariff is null)
            {
                throw new NotFoundException($"Billboard with id: {request.TariffId} not found");
            }

            return tariff.CreateResponse();
        }
    }
}