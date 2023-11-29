using Application.Extensions;
using Application.InternalModels;
using Application.Services;
using Contracts.Exceptions;
using Contracts.Requests;
using Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Enums;
using Persistence.Models;

namespace Application.CQRS.Commands;

public class AddBillboardCommand : IRequest<BillboardResponse>
{
    public required AddBillboardRequest Request { get; init; }

    public class AddBillboardCommandHandler : IRequestHandler<AddBillboardCommand, BillboardResponse>
    {
        private readonly BillboardContext _context;
        private readonly IMediaFileProvider _fileProvider;

        public AddBillboardCommandHandler(BillboardContext context, IMediaFileProvider fileProvider)
        {
            _context = context;
            _fileProvider = fileProvider;
        }

        public async Task<BillboardResponse> Handle(AddBillboardCommand request, CancellationToken cancellationToken)
        {
            var billboardType = await _context.BillboardSurfaces.FirstOrDefaultAsync(
                e => e.Id == request.Request.BillboardSurfaceId, cancellationToken);
            var groupOfTariffs = await _context.GroupOfTariffs.FirstOrDefaultAsync(
                    e => e.Id == request.Request.GroupOfTariffs, cancellationToken);
            if (billboardType is null)
            {
                throw new NotFoundException($"Billboard surface with id {request.Request.BillboardSurfaceId} not found");
            }

            if (groupOfTariffs is null)
            {
                throw new NotFoundException($"Group of tariffs with id {request.Request.GroupOfTariffs} not found");
            }

            var files = new List<MediaFile>();
            foreach (var picture in request.Request.Pictures)
            {
                var file = await _fileProvider.WriteFileAsync(picture, cancellationToken);
                files.Add(file);
            }

            var billboard = new Billboard
            {
                Name = request.Request.Name,
                Description = request.Request.Description,
                Address = request.Request.Address,
                TypeId = Enum.Parse<BillboardTypeId>(request.Request.BillboardType),
                BillboardSurface = billboardType,
                Width = request.Request.Width,
                Height = request.Request.Height,
                Penalty = request.Request.Penalty,
                GroupOfTariffs = groupOfTariffs,
                ArchiveStatusId = ArchiveStatusId.NonArchived,
                Pictures = files
                    .Select(e => new Picture
                    {
                        Source = e.Path
                    })
                    .ToList()
            };
            await _context.Billboards.AddAsync(billboard, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return billboard.CreateResponse();
        }
    }
}