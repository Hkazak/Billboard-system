using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetBillboardTypesQuery : IRequest<IEnumerable<string>>
{
    public class GetBillboardTypesQueryHandler : IRequestHandler<GetBillboardTypesQuery, IEnumerable<string>>
    {
        private readonly BillboardContext _context;

        public GetBillboardTypesQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> Handle(GetBillboardTypesQuery request, CancellationToken cancellationToken)
        {
            return await _context.BillboardTypes.Select(e => e.Type).ToListAsync(cancellationToken);
        }
    }
}