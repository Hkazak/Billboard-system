using Contracts.Exceptions;
using MediatR;
using Persistence.Context;
using Persistence.Models;

namespace Application.CQRS.Queries;

public class GetUserInformationQuery : IRequest<User>
{
    public required Guid UserId { get; init; }

    public class GetUserInformationQueryHandler : IRequestHandler<GetUserInformationQuery, User>
    {
        private readonly BillboardContext _context;

        public GetUserInformationQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<User> Handle(GetUserInformationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}