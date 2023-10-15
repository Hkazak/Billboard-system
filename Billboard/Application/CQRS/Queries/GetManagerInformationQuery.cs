using Application.Extensions;
using Contracts.Exceptions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;

namespace Application.CQRS.Queries;

public class GetManagerInformationQuery : IRequest<ManagerResponse>
{
    public required Guid ManagerId { get; init; }

    public class GetUserInformationQueryHandler : IRequestHandler<GetManagerInformationQuery, ManagerResponse>
    {
        private readonly BillboardContext _context;

        public GetUserInformationQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<ManagerResponse> Handle(GetManagerInformationQuery request, CancellationToken cancellationToken)
        {
            var manager = await _context.Managers.FirstOrDefaultAsync(e => e.Id == request.ManagerId && e.StatusId == ManagerStatusId.Active, cancellationToken);
            if (manager is null)
            {
                throw new NotFoundException($"Manager with id: {request.ManagerId} not found");
            }

            return manager.CreateResponse();
        }
    }
}