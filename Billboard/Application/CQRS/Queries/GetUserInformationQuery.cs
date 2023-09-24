using Application.Extensions;
using Contracts.Exceptions;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetUserInformationQuery : IRequest<UserResponse>
{
    public required Guid UserId { get; init; }

    public class GetUserInformationQueryHandler : IRequestHandler<GetUserInformationQuery, UserResponse>
    {
        private readonly BillboardContext _context;

        public GetUserInformationQueryHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<UserResponse> Handle(GetUserInformationQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Id == request.UserId, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException($"User with id: {request.UserId} not found");
            }

            return user.CreateResponse();
        }
    }
}