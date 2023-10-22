using Application.InternalModels;
using Application.Services;
using Contracts.Configurations;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace Application.ServicesImplementations;

public sealed class EmailService : IEmailService
{
    private readonly EmailConfiguration _configuration;
    private readonly Lazy<Task<ISmtpClient>> _smtpClientLazy;

    public EmailService(EmailConfiguration configuration, ISmtpClient smtpClient)
    {
        _configuration = configuration;
        _smtpClientLazy = new Lazy<Task<ISmtpClient>>(async () =>
        {
            await smtpClient.ConnectAsync(_configuration.Host, _configuration.Port, _configuration.UseSsl);
            await smtpClient.AuthenticateAsync(_configuration.EmailAddress, _configuration.Password);
            return smtpClient;
        });
    }

    public async Task SendMessageAsync(EmailMessage message, CancellationToken cancellationToken = default)
    {
        using var emailMessage = new MimeMessage();
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(TextFormat.Html)
        {
            Text = message.Text
        };
        emailMessage.To.Add(new MailboxAddress(string.Empty, message.ReceiverEmailAddress));
        emailMessage.From.Add(new MailboxAddress(_configuration.Nickname, _configuration.EmailAddress));
        var client = await _smtpClientLazy.Value;
        await client.SendAsync(emailMessage, cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        var client = await _smtpClientLazy.Value;
        await client.DisconnectAsync(true);
        client.Dispose();
    }
}