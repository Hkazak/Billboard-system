using Application.Extensions;
using Application.Services;
using Contracts.Exceptions;
using Contracts.Requests;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;

namespace Application.CQRS.Queries;

public class SigninManagerQuery : IRequest<AuthTokenResponse>
{
    public required SigninRequest Request { get; init; }
    
    public class SigninQueryHandler : IRequestHandler<SigninManagerQuery, AuthTokenResponse>
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

        public async Task<AuthTokenResponse> Handle(SigninManagerQuery request, CancellationToken cancellationToken)
        {
            var passwordHash = _passwordHasher.CalculateHash(request.Request.Password);
            var manager = await _context.Managers.FirstOrDefaultAsync(
                e => e.Email == request.Request.Email && e.Password == passwordHash && e.StatusId == ManagerStatusId.Active, cancellationToken);
            if (manager is null)
            {
                throw new NotFoundException($"Manager with email {request.Request.Email} not found");
            }

            var token = _authenticationService.GenerateJwtToken(manager.CreateClaims());
            return token;
        }
    }
}