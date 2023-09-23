using Contracts.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Id == request.UserId, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException($"User with id: {request.UserId} not found");
            }

            return user;
        }
    }
}