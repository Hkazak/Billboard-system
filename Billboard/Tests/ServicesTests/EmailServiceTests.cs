using Application.InternalModels;
using Application.ServicesImplementations;
using Contracts.Configurations;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using Moq;
using NUnit.Framework;

namespace Tests.ServicesTests;

public class EmailServiceTests
{
    [Test]
    public async Task SendMessageAsync_ValidMessage_MessagesAreEqual()
    {
        var configuration = new EmailConfiguration
        {
            EmailAddress = "testMail@fake.ru",
            Password = "P@ssw0rd",
            Nickname = "testName",
            Host = "smtp.testhost.com",
            Port = 589,
            UseSsl = true
        };
        EmailMessage? sentMessage = null;
        var smtpClientMock = new Mock<ISmtpClient>();
        smtpClientMock.Setup(e => e.ConnectAsync(configuration.Host, configuration.Port, configuration.UseSsl, It.IsAny<CancellationToken>()));
        smtpClientMock.Setup(e => e.AuthenticateAsync(configuration.EmailAddress, configuration.Password, It.IsAny<CancellationToken>()));
        smtpClientMock.Setup(e => e.SendAsync(It.IsAny<MimeMessage>(), It.IsAny<CancellationToken>(), It.IsAny<ITransferProgress>()))
            .Callback((MimeMessage msg, CancellationToken _, ITransferProgress _) =>
            {
                sentMessage = new EmailMessage
                {
                    ReceiverEmailAddress = msg.To.First().ToString() ?? string.Empty,
                    Subject = msg.Subject,
                    Text = msg.HtmlBody
                };
            })
            .ReturnsAsync("Successfully sent");
        var message = new EmailMessage
        {
            ReceiverEmailAddress = "testReceiver@fake.com",
            Subject = "Refined code",
            Text = "Your code 8888"
        };
        var service = new EmailService(configuration, smtpClientMock.Object);
        await service.SendMessageAsync(message, CancellationToken.None);
        Assert.That(sentMessage, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(sentMessage!.Subject, Is.EqualTo(message.Subject));
            Assert.That(sentMessage!.ReceiverEmailAddress, Is.EqualTo(message.ReceiverEmailAddress));
            Assert.That(sentMessage!.Text, Is.EqualTo(message.Text));
        });
    }
    
    [Test]
    public async Task SendMessageAsync_ValidHtmlMessage_FormattedMessage()
    {
        var configuration = new EmailConfiguration
        {
            EmailAddress = "testMail@fake.ru",
            Password = "P@ssw0rd",
            Nickname = "testName",
            Host = "smtp.testhost.com",
            Port = 589,
            UseSsl = true
        };
        EmailMessage? sentMessage = null;
        var smtpClientMock = new Mock<ISmtpClient>();
        smtpClientMock.Setup(e => e.ConnectAsync(configuration.Host, configuration.Port, configuration.UseSsl, It.IsAny<CancellationToken>()));
        smtpClientMock.Setup(e => e.AuthenticateAsync(configuration.EmailAddress, configuration.Password, It.IsAny<CancellationToken>()));
        smtpClientMock.Setup(e => e.SendAsync(It.IsAny<MimeMessage>(), It.IsAny<CancellationToken>(), It.IsAny<ITransferProgress>()))
            .Callback((MimeMessage msg, CancellationToken _, ITransferProgress _) =>
            {
                sentMessage = new EmailMessage
                {
                    ReceiverEmailAddress = msg.To.First().ToString() ?? string.Empty,
                    Subject = msg.Subject,
                    Text = msg.HtmlBody
                };
            })
            .ReturnsAsync("Successfully sent");
        var message = new EmailMessage
        {
            ReceiverEmailAddress = "testReceiver@fake.com",
            Subject = "Refined code",
            Text = "<p>Your code 8888<p>"
        };
        var service = new EmailService(configuration, smtpClientMock.Object);
        await service.SendMessageAsync(message, CancellationToken.None);
        Assert.That(sentMessage, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(sentMessage!.Subject, Is.EqualTo(message.Subject));
            Assert.That(sentMessage!.ReceiverEmailAddress, Is.EqualTo(message.ReceiverEmailAddress));
            Assert.That(sentMessage!.Text, Is.EqualTo(message.Text));
        });
    }
}