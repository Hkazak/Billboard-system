using Contracts.Requests;
using Contracts.Responses;
using MediatR;
using Persistence.Context;

namespace Application.CQRS.Commands;

public class AddUserCommand : IRequest<AuthTokenResponse>
{
    public required SignupRequest Request { get; init; }
    
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, AuthTokenResponse>
    {
        private readonly BillboardContext _context;

        public AddUserCommandHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<AuthTokenResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}