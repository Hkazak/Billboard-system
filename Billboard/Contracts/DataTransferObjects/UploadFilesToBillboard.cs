namespace Contracts.DataTransferObjects;

public record UploadFilesToBillboard
{
    public required Guid BillboardId { get; init; }
    public IEnumerable<Stream> Files { get; init; } = Enumerable.Empty<Stream>();
}