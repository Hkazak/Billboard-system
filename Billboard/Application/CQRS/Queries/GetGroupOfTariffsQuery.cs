using Application.Extensions;
using Contracts.Exceptions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Models;

namespace Application.CQRS.Queries;

public class GetGroupOfTariffsQuery : IRequest<GroupOfTariffsResponse>
{
    public required Guid Id { get; init; }
    
    public class GetGroupOfTariffsQueryHandler : IRequestHandler<GetGroupOfTariffsQuery, GroupOfTariffsResponse>
    {
        private readonly BillboardContext _context;

        public GetGroupOfTariffsQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<GroupOfTariffsResponse> Handle(GetGroupOfTariffsQuery request,
            CancellationToken cancellationToken)
        {
            var groupOfTariffs =
                await _context.GroupOfTariffs.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (groupOfTariffs is null)
            {
                throw new NotFoundException($"Billboard with id: {request.Id} not found");
            }

            return groupOfTariffs.CreateResponse();
        }
    }
}