using Application.CQRS.Queries;
using Application.InternalModels;
using Application.Services;
using Contracts.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using NUnit.Framework;
using Persistence.Context;
using Tests.TestsHelpers;

namespace Tests.CQRSTests.Queries;

public class ResetPasswordQueryTests
{
    private BillboardContext _context = default!;
    private IEmailService _emailService = default!;
    private IDistributedCache _distributedCache = default!;

    [OneTimeSetUp]
    public async Task SetupContext()
    {
        var options = new DbContextOptionsBuilder<BillboardContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new BillboardContext(options);
        await _context.SeedManagersAsync();
        await _context.SeedUsersAsync();
    }

    [OneTimeSetUp]
    public void SetupMocks()
    {
        var emailServiceMock = new Mock<IEmailService>();
        emailServiceMock.Setup(e => e.SendMessageAsync(It.IsAny<EmailMessage>(), It.IsAny<CancellationToken>()));
        var distributedCacheMock = new Mock<IDistributedCache>();
        distributedCacheMock.Setup(e =>
            e.SetAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()));
        _emailService = emailServiceMock.Object;
        _distributedCache = distributedCacheMock.Object;
    }

    [Test]
    public async Task Handle_ExistedUser_ValidSendingCode()
    {
        var user = await _context.Users.FirstAsync();
        var query = new ResetPasswordQuery
        {
            Email = user.Email
        };
        var handler = new ResetPasswordQuery.ResetPasswordQueryHandler(_context, _emailService, _distributedCache);
        Assert.DoesNotThrowAsync(async () => await handler.Handle(query, CancellationToken.None));
    }
    
    [Test]
    public async Task Handle_ExistedManager_ValidSendingCode()
    {
        var manager = await _context.Managers.FirstAsync();
        var query = new ResetPasswordQuery
        {
            Email = manager.Email
        };
        var handler = new ResetPasswordQuery.ResetPasswordQueryHandler(_context, _emailService, _distributedCache);
        Assert.DoesNotThrowAsync(async () => await handler.Handle(query, CancellationToken.None));
    }
    
    [Test]
    public void Handle_NotExistedPerson_ValidSendingCode()
    {
        var query = new ResetPasswordQuery
        {
            Email = "  @fake.com"
        };
        var handler = new ResetPasswordQuery.ResetPasswordQueryHandler(_context, _emailService, _distributedCache);
        Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
    }

    [OneTimeTearDown]
    public async Task DisposeContext()
    {
        await _context.DisposeAsync();
    }
}