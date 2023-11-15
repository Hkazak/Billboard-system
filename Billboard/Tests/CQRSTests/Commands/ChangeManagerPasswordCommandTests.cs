﻿using Application.CQRS.Commands;
using Application.Services;
using Contracts.DataTransferObjects;
using Contracts.Exceptions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Persistence.Context;
using Persistence.Enums;
using Tests.TestsHelpers;

namespace Tests.CQRSTests.Commands;

public class ChangeManagerPasswordCommandTests
{
    private BillboardContext _context = default!;
    private IPasswordHasher _passwordHasher = default!;

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
        var passwordHasherMock = new Mock<IPasswordHasher>();
        passwordHasherMock.Setup(e => e.CalculateHash("P@ssw0rd"))
            .Returns("b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342");
        passwordHasherMock.Setup(e => e.CalculateHash("P@ssw0rd!"))
            .Returns("0e44ce7308af2b3de5232e4616403ce7d49ba2aec83f79c196409556422a4927");
        passwordHasherMock.Setup(e => e.CalculateHash(It.Is<string>(password => password != "P@ssw0rd" && password != "P@ssw0rd!")))
            .Returns("0e69e6a4038df88d4c62c837edd7e04a95ea6368bca9d469e00ad766a2266770");
        _passwordHasher = passwordHasherMock.Object;
    }

    [Test]
    public async Task Handle_ExistedManager_PasswordSuccessfullyChanged()
    {
        var manager = await _context.Managers.FirstAsync();
        const string expectedPasswordHash = "0e44ce7308af2b3de5232e4616403ce7d49ba2aec83f79c196409556422a4927";
        var request = new ChangePassword
        {
            Id = manager.Id,
            OldPassword = "P@ssw0rd",
            NewPassword = "P@ssw0rd!"
        };
        var command = new ChangeManagerPasswordCommand
        {
            NewData = request
        };
        var handler = new ChangeManagerPasswordCommand.ChangeManagerManagerPasswordCommandHandler(_context, _passwordHasher);
        Assert.DoesNotThrowAsync(async () => await handler.Handle(command, CancellationToken.None));
        Assert.That(manager.Password, Is.EqualTo(expectedPasswordHash));
    }
    
    [Test]
    public void Handle_NotExistedManager_ThrowsInvalidCredentialsException()
    {
        var request = new ChangePassword
        {
            Id = Guid.Empty,
            OldPassword = "P@ssw0rd",
            NewPassword = "P@ssw0rd"
        };
        var command = new ChangeManagerPasswordCommand
        {
            NewData = request
        };
        var handler = new ChangeManagerPasswordCommand.ChangeManagerManagerPasswordCommandHandler(_context, _passwordHasher);
        Assert.ThrowsAsync<InvalidCredentialsException>(async () => await handler.Handle(command, CancellationToken.None));
    }
    
    [Test]
    public async Task Handle_ExistedManagerWithIncorrectPassword_ThrowsInvalidCredentialsException()
    {
        var manager = await _context.Managers.LastAsync();
        var request = new ChangePassword
        {
            Id = manager.Id,
            OldPassword = "P@ssw0rd!",
            NewPassword = "P@ssw0rd"
        };
        var command = new ChangeManagerPasswordCommand
        {
            NewData = request
        };
        var handler = new ChangeManagerPasswordCommand.ChangeManagerManagerPasswordCommandHandler(_context, _passwordHasher);
        Assert.ThrowsAsync<InvalidCredentialsException>(async () => await handler.Handle(command, CancellationToken.None));
    }
    
    [Test]
    public async Task Handle_ExistedInactiveManager_ThrowsInvalidCredentialsException()
    {
        var manager = await _context.Managers.LastAsync(e => e.StatusId == ManagerStatusId.Inactive);
        var request = new ChangePassword
        {
            Id = manager.Id,
            OldPassword = "P@ssw0rd",
            NewPassword = "P@ssw0rd!"
        };
        var command = new ChangeManagerPasswordCommand
        {
            NewData = request
        };
        var handler = new ChangeManagerPasswordCommand.ChangeManagerManagerPasswordCommandHandler(_context, _passwordHasher);
        Assert.ThrowsAsync<InvalidCredentialsException>(async () => await handler.Handle(command, CancellationToken.None));
    }

    [OneTimeTearDown]
    public async Task DisposeContext()
    {
        await _context.DisposeAsync();
    }
}