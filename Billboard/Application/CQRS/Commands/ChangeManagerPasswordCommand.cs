using Application.Services;
using Contracts.DataTransferObjects;
using Contracts.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Commands;

public class ChangeManagerPasswordCommand : IRequest
{
    public required ChangePassword NewData { get; init; }
    
    public class ChangeManagerManagerPasswordCommandHandler : IRequestHandler<ChangeManagerPasswordCommand>
    {
        private readonly BillboardContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public ChangeManagerManagerPasswordCommandHandler(BillboardContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(ChangeManagerPasswordCommand request, CancellationToken cancellationToken)
        {
            var currentPasswordHash = _passwordHasher.CalculateHash(request.NewData.OldPassword);
            var user = await _context.Managers.FirstOrDefaultAsync(
                e => e.Id == request.NewData.Id && e.Password == currentPasswordHash, cancellationToken);
            if (user is null)
            {
                throw new InvalidCredentialsException("Incorrect password");
            }

            var newPasswordHash = _passwordHasher.CalculateHash(request.NewData.NewPassword);
            user.Password = newPasswordHash;
            _context.Managers.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}