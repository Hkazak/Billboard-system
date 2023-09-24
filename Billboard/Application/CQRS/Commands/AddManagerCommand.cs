using Application.Extensions;
using Contracts.Requests;
using Contracts.Responses;
using MediatR;
using Persistence.Context;
using Persistence.Enums;
using Persistence.Models;

namespace Application.CQRS.Commands;

public class AddManagerCommand : IRequest<UserResponse>
{
    public required AddManagerRequest Request { get; init; }

    public class AddManagerCommandHandler : IRequestHandler<AddManagerCommand, UserResponse>
    {
        private readonly BillboardContext _context;

        public AddManagerCommandHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<UserResponse> Handle(AddManagerCommand request, CancellationToken cancellationToken)
        {
            const string defaultPasswordHash = "b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342"; // Password: P@ssw0rd
            var user = new User
            {
                Name = request.Request.Name,
                Email = request.Request.Email,
                Password = defaultPasswordHash,
                RoleId = UserRoleId.Manager
            };
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return user.CreateResponse();
        }
    }
}