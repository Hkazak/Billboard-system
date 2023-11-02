using Application.InternalModels;
using Application.Services;
using Contracts.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class ResetPasswordQuery : IRequest
{
    public required string Email { get; init; }
    
    public class ResetPasswordQueryHandler : IRequestHandler<ResetPasswordQuery>
    {
        private readonly BillboardContext _context;
        private readonly IEmailService _emailService;
        private readonly IDistributedCache _distributedCache;

        public ResetPasswordQueryHandler(BillboardContext context, IEmailService emailService, IDistributedCache distributedCache)
        {
            _context = context;
            _emailService = emailService;
            _distributedCache = distributedCache;
        }

        public async Task Handle(ResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Email == request.Email , cancellationToken);
            if (user is null)
            {
                throw new NotFoundException($"User {request.Email} not found");
            }
            
            var randomCode = Random.Shared.Next(1000, 9999);
            await _distributedCache.SetStringAsync(user.Email, randomCode.ToString(),cancellationToken);
            var message = new EmailMessage
            {
                ReceiverEmailAddress = request.Email,
                Subject = "Reset password",
                Text = $"<span>Your confirmation code is : {randomCode}"
            };
            await _emailService.SendMessageAsync(message, cancellationToken);
        }
    }
}