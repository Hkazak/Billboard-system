namespace Contracts.DataTransferObjects;

public record OrderFile
{
    public required byte[] Data { get; init; }
}