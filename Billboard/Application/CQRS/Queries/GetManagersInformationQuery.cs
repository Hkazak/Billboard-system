using Application.Extensions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetManagersInformationQuery : IRequest<IEnumerable<ManagerResponse>>
{
    public class GetManagersInformationQueryHandler : IRequestHandler<GetManagersInformationQuery, IEnumerable<ManagerResponse>>
    {
        private readonly BillboardContext _context;

        public GetManagersInformationQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ManagerResponse>> Handle(GetManagersInformationQuery request, CancellationToken cancellationToken)
        {
            var managers = await _context.Managers.ToListAsync(cancellationToken);

            return managers.Select(e => e.CreateResponse());
        }
    }
}