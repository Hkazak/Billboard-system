using Application.InternalModels;

namespace Application.Services;

public interface IEmailService
{
    Task SendMessageAsync(EmailMessage message, CancellationToken cancellationToken = default);
}