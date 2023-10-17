using Application.InternalModels;
using Application.Services;
using Contracts.Configurations;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace Application.ServicesImplementations;

public class EmailService : IEmailService
{
    private readonly EmailConfiguration _configuration;

    public EmailService(EmailConfiguration configuration)
    {
        _configuration = configuration;
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
        using var client = new SmtpClient();
        await client.ConnectAsync(_configuration.Host, _configuration.Port, _configuration.UseSsl, cancellationToken);
        await client.AuthenticateAsync(_configuration.EmailAddress, _configuration.Password, cancellationToken);
        await client.SendAsync(emailMessage, cancellationToken);
        await client.DisconnectAsync(true, cancellationToken);
    }
}