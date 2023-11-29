using Application.InternalModels;
using Application.Services;

namespace Application.ServicesImplementations;

public class UploadedFileProvider : IMediaFileProvider
{
    private readonly string _folderPath;

    public UploadedFileProvider(string folderPath)
    {
        _folderPath = folderPath;
    }

    public async Task<MediaFile> WriteFileAsync(string base64String, CancellationToken cancellationToken = default)
    {
        var name = $"{Guid.NewGuid()}.jpg";
        var path = $"{_folderPath}/{name}";
        var data = Convert.FromBase64String(base64String);
        await File.WriteAllBytesAsync(path, data, cancellationToken);
        return new MediaFile
        {
            Name = name,
            Path = path
        };
    }
}