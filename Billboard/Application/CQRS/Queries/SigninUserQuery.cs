using Application.Extensions;
using Application.Services;
using Contracts.Exceptions;
using Contracts.Requests;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class SigninUserQuery : IRequest<AuthTokenResponse>
{
    public required SigninRequest Request { get; init; }
    
    public class SigninQueryHandler : IRequestHandler<SigninUserQuery, AuthTokenResponse>
    {
        private readonly BillboardContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthenticationService _authenticationService;

        public SigninQueryHandler(BillboardContext context, IPasswordHasher passwordHasher,
            IAuthenticationService authenticationService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationService = authenticationService;
        }

        public async Task<AuthTokenResponse> Handle(SigninUserQuery request, CancellationToken cancellationToken)
        {
            var passwordHash = _passwordHasher.CalculateHash(request.Request.Password);
            var user = await _context.Users.FirstOrDefaultAsync(
                e => e.Email == request.Request.Email && e.Password == passwordHash, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException($"User with email {request.Request.Email} not found");
            }

            var token = _authenticationService.GenerateJwtToken(user.CreateClaims());
            return token;
        }
    }
}