using Application.InternalModels;

namespace Application.Services;

public interface IMediaFileProvider
{
    Task<MediaFile> WriteFileAsync(string base64String, CancellationToken cancellationToken = default);
}