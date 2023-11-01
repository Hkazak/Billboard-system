using Application.Services;
using Contracts.DataTransferObjects;
using Contracts.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Persistence.Context;

namespace Application.CQRS.Commands;

public class ResetPasswordCommand : IRequest
{
    public required CodeConfirmation CodeConfirmationRequest { get; init; }

    public class ResetPasswordRequestCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly IDistributedCache _distributedCache;
        private readonly BillboardContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public ResetPasswordRequestCommandHandler(IDistributedCache distributedCache, BillboardContext context, IPasswordHasher passwordHasher)
        {
            _distributedCache = distributedCache;
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var confirmationCode = await _distributedCache.GetStringAsync(request.CodeConfirmationRequest.Email, cancellationToken);
            if (confirmationCode is null)
            {
                throw new InvalidOperationException($"Cache has no confirmation code for user {request.CodeConfirmationRequest.Email}");
            }
            
            if (confirmationCode == request.CodeConfirmationRequest.ConfirmationCode)
            {
                var user = await _context.Users.FirstOrDefaultAsync(
                    e => e.Email == request.CodeConfirmationRequest.Email , cancellationToken);
                if (user is null)
                {
                    throw new NotFoundException($"User {request.CodeConfirmationRequest.Email} not found");
                }

                var newPasswordHash = _passwordHasher.CalculateHash(request.CodeConfirmationRequest.NewPassword);
                user.Password = newPasswordHash;
                _context.Users.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new InvalidCredentialsException("Incorrect code");
            }
        }
    }
}