using System.Text;
using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Application.InternalModels;
using Application.Services;
using Contracts.DataTransferObjects;
using Contracts.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using NUnit.Framework;
using Persistence.Context;
using Tests.TestsHelpers;

namespace Tests.CQRSTests.Commands;

public class ResetPasswordCommandTests
{
    private BillboardContext _context = default!;
    private IPasswordHasher _passwordHasher = default!;
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
        var passwordHasherMock = new Mock<IPasswordHasher>();
        passwordHasherMock.Setup(e => e.CalculateHash("P@ssw0rd"))
            .Returns("b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342");
        passwordHasherMock.Setup(e => e.CalculateHash(It.Is<string>(password => password != "P@ssw0rd")))
            .Returns("0e69e6a4038df88d4c62c837edd7e04a95ea6368bca9d469e00ad766a2266770");
        var distributedCacheMock = new Mock<IDistributedCache>();
        distributedCacheMock.Setup(e => e.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync("0000"u8.ToArray());
        _passwordHasher = passwordHasherMock.Object;
        _distributedCache = distributedCacheMock.Object;
    }

    [Test]
    public async Task Handle_ExistedUser_ValidChangingPassword()
    {
        var user = await _context.Users.FirstAsync();
        var request = new CodeConfirmation
        {
            Email = user.Email,
            NewPassword = "P@ssw0rd",
            ConfirmationCode = "0000"
        };
        var command = new ResetPasswordCommand
        {
            CodeConfirmationRequest = request
        };
        var handler = new ResetPasswordCommand.ResetPasswordRequestCommandHandler(_distributedCache, _context, _passwordHasher);
        Assert.DoesNotThrowAsync(async () => await handler.Handle(command, CancellationToken.None));
    }
    
    [Test]
    public async Task Handle_ExistedManager_ValidChangingPassword()
    {
        var manager = await _context.Managers.FirstAsync();
        var request = new CodeConfirmation
        {
            Email = manager.Email,
            NewPassword = "P@ssw0rd",
            ConfirmationCode = "0000"
        };
        var query = new ResetPasswordCommand
        {
            CodeConfirmationRequest = request
        };
        var handler = new ResetPasswordCommand.ResetPasswordRequestCommandHandler(_distributedCache, _context, _passwordHasher);
        Assert.DoesNotThrowAsync(async () => await handler.Handle(query, CancellationToken.None));
    }
    
    [Test]
    public void Handle_NotExistedUser_ThrowsNotFoundException()
    {
        var request = new CodeConfirmation
        {
            Email = "   @fake.com",
            NewPassword = "P@ssw0rd",
            ConfirmationCode = "0000"
        };
        var command = new ResetPasswordCommand
        {
            CodeConfirmationRequest = request
        };
        var handler = new ResetPasswordCommand.ResetPasswordRequestCommandHandler(_distributedCache, _context, _passwordHasher);
        Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public async Task Handle_ExistedUserWithIncorrectCode_ThrowsInvalidCredentialsException()
    {
        var user = await _context.Users.FirstAsync();
        var request = new CodeConfirmation
        {
            Email = user.Email,
            NewPassword = "P@ssw0rd",
            ConfirmationCode = "0001"
        };
        var command = new ResetPasswordCommand
        {
            CodeConfirmationRequest = request
        };
        var handler = new ResetPasswordCommand.ResetPasswordRequestCommandHandler(_distributedCache, _context, _passwordHasher);
        Assert.ThrowsAsync<InvalidCredentialsException>(async () => await handler.Handle(command, CancellationToken.None));
    }
    
    [Test]
    public async Task Handle_ExistedManagerWithIncorrectCode_ThrowsInvalidCredentialsException()
    {
        var manager = await _context.Managers.FirstAsync();
        var request = new CodeConfirmation
        {
            Email = manager.Email,
            NewPassword = "P@ssw0rd",
            ConfirmationCode = "0001"
        };
        var query = new ResetPasswordCommand
        {
            CodeConfirmationRequest = request
        };
        var handler = new ResetPasswordCommand.ResetPasswordRequestCommandHandler(_distributedCache, _context, _passwordHasher);
        Assert.ThrowsAsync<InvalidCredentialsException>(async () => await handler.Handle(query, CancellationToken.None));
    }

    [OneTimeTearDown]
    public async Task DisposeContext()
    {
        await _context.DisposeAsync();
    }
}