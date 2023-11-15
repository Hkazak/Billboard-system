using Application.CQRS.Commands;
using Application.InternalModels;
using Application.Services;
using Contracts.Requests;
using Contracts.Responses;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Persistence.Context;
using Tests.TestsHelpers;

namespace Tests.CQRSTests.Commands;

public class AddUserCommandTests
{
    private BillboardContext _context = default!;
    private IPasswordHasher _passwordHasher = default!;
    private IAuthenticationService _authenticationService = default!;

    [OneTimeSetUp]
    public async Task SetupContext()
    {
        var options = new DbContextOptionsBuilder<BillboardContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new BillboardContext(options);
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
        var authenticationServiceMock = new Mock<IAuthenticationService>();
        authenticationServiceMock.Setup(e => e.GenerateJwtToken(It.IsAny<AuthenticationClaims>()))
            .Returns(new AuthTokenResponse
            {
                AccessToken = "valid token"
            });
        _passwordHasher = passwordHasherMock.Object;
        _authenticationService = authenticationServiceMock.Object;
    }

    [Test]
    public async Task Handle_ValidUser_ValidToken()
    {
        const string expectedToken = "valid token";
        var request = new SignupRequest
        {
            Email = "fake@fake.com",
            Name = "someuser",
            Password = "P@ssw0rd",
            ConfirmPassword = "P@ssw0rd"
        };
        var command = new AddUserCommand
        {
            Request = request
        };
        var handler = new AddUserCommand.AddUserCommandHandler(_context, _passwordHasher, _authenticationService);
        var response = await handler.Handle(command, CancellationToken.None);
        Assert.That(response.AccessToken, Is.EqualTo(expectedToken));
    }

    [OneTimeTearDown]
    public async Task DisposeContext()
    {
        await _context.DisposeAsync();
    }
}