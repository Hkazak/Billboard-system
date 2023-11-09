using Application.CQRS.Commands;
using Application.InternalModels;
using Application.Services;
using Contracts.Requests;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Persistence.Context;
using Tests.TestsHelpers;

namespace Tests.CQRSTests;

public class AddManagerCommandTests
{
    private BillboardContext _context = default!;
    private IEmailService _emailService = default!;

    [OneTimeSetUp]
    public async Task SetupContext()
    {
        var options = new DbContextOptionsBuilder<BillboardContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new BillboardContext(options);
        await _context.SeedManagersAsync();
    }

    [OneTimeSetUp]
    public void SetupMocks()
    {
        var emailServiceMock = new Mock<IEmailService>();
        emailServiceMock.Setup(e => e.SendMessageAsync(It.IsAny<EmailMessage>(), It.IsAny<CancellationToken>()));
        _emailService = emailServiceMock.Object;
    }

    [Test]
    public async Task Handle_ValidManager_SuccessfullyAdded()
    {
        var request = new AddManagerRequest
        {
            Email = "fake@fake.com",
            FirstName = "fakeName",
            MiddleName = "fakeMiddleName",
            LastName = "fakeSurname",
            Phone = "fakePhone"
        };
        var command = new AddManagerCommand
        {
            Request = request
        };
        var handler = new AddManagerCommand.AddManagerCommandHandler(_context, _emailService);
        var response = await handler.Handle(command, CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(response.Email, Is.EqualTo(request.Email));
            Assert.That(response.FirstName, Is.EqualTo(request.FirstName));
            Assert.That(response.LastName, Is.EqualTo(request.LastName));
            Assert.That(response.MiddleName, Is.EqualTo(request.MiddleName));
            Assert.That(response.Phone, Is.EqualTo(request.Phone));
        });
    }

    [OneTimeTearDown]
    public async Task DisposeContext()
    {
        await _context.DisposeAsync();
    }
}