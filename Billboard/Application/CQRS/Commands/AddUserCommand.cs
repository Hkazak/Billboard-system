using Application.Extensions;
using Application.Services;
using Contracts.Requests;
using Contracts.Responses;
using MediatR;
using Persistence.Context;
using Persistence.Enums;
using Persistence.Models;

namespace Application.CQRS.Commands;

public class AddUserCommand : IRequest<AuthTokenResponse>
{
    public required SignupRequest Request { get; init; }
    
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, AuthTokenResponse>
    {
        private readonly BillboardContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthenticationService _authenticationService;

        public AddUserCommandHandler(BillboardContext context, IPasswordHasher passwordHasher, IAuthenticationService authenticationService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationService = authenticationService;
        }

        public async Task<AuthTokenResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _passwordHasher.CalculateHash(request.Request.Password);
            var user = new User
            {
                Name = request.Request.Name,
                Email = request.Request.Email,
                Password = passwordHash,
                RoleId = UserRoleId.Client
            };
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            var token = _authenticationService.GenerateJwtToken(user.CreateClaims());

            return token;
        }
    }
}