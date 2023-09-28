using Application.Extensions;
using Contracts.Requests;
using Contracts.Responses;
using MediatR;
using Persistence.Context;
using Persistence.Models;

namespace Application.CQRS.Commands;

public class AddManagerCommand : IRequest<ManagerResponse>
{
    public required AddManagerRequest Request { get; init; }

    public class AddManagerCommandHandler : IRequestHandler<AddManagerCommand, ManagerResponse>
    {
        private readonly BillboardContext _context;

        public AddManagerCommandHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task<ManagerResponse> Handle(AddManagerCommand request, CancellationToken cancellationToken)
        {
            const string defaultPasswordHash = "b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342"; // Password: P@ssw0rd
            var manager = new Manager
            {
                Email = request.Request.Email,
                Password = defaultPasswordHash,
                Phone = request.Request.Phone,
                FirstName = request.Request.FirstName,
                MiddleName = request.Request.MiddleName,
                LastName = request.Request.LastName,
            };
            await _context.Managers.AddAsync(manager, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return manager.CreateResponse();
        }
    }
}