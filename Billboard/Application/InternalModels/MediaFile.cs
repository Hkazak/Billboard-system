namespace Application.InternalModels;

public record MediaFile
{
    public required string Name { get; init; }
    public required string Path { get; init; }
}