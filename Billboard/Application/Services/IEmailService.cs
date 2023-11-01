using Application.InternalModels;

namespace Application.Services;

public interface IEmailService : IAsyncDisposable
{
    Task SendMessageAsync(EmailMessage message, CancellationToken cancellationToken = default);
}