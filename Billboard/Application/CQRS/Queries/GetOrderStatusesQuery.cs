using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetOrdersStatusesQuery : IRequest<IEnumerable<string>>
{
    public class GetOrdersStatusesQueryHandler : IRequestHandler<GetOrdersStatusesQuery, IEnumerable<string>>
    {
        private readonly BillboardContext _context;

        public GetOrdersStatusesQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> Handle(GetOrdersStatusesQuery request, CancellationToken cancellationToken)
        {
            var statuses = await _context.OrderStatusEnumerable.ToListAsync(cancellationToken);
            return statuses.Select(e => e.Id.ToString());
        }
    }
}