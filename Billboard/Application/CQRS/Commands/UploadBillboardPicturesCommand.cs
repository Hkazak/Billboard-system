using MediatR;
using Persistence.Context;

namespace Application.CQRS.Commands;

public class UploadBillboardPicturesCommand : IRequest
{
    public required IEnumerable<Stream> Files { get; init; }

    public class UploadBillboardPicturesCommandHandler : IRequestHandler<UploadBillboardPicturesCommand>
    {
        private readonly BillboardContext _context;

        public UploadBillboardPicturesCommandHandler(BillboardContext context)
        {
            _context = context;
        }

        public async Task Handle(UploadBillboardPicturesCommand request, CancellationToken cancellationToken)
        {
            var files = request.Files;
            foreach (var stream in files)
            {
                await using var fileStream = File.OpenWrite($"{Guid.NewGuid().ToString()}.jpg");
                await stream.CopyToAsync(fileStream, cancellationToken);
                await fileStream.FlushAsync(cancellationToken);
                await stream.DisposeAsync();
            }
        }
    }
}