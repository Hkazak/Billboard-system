namespace Application.InternalModels;

public record EmailMessage
{
    public required string ReceiverEmailAddress { get; init; }
    public required string Subject { get; init; }
    public required string Text { get; init; }
}