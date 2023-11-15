using Application.CQRS.Queries;
using Application.InternalModels;
using Application.Services;
using Contracts.Exceptions;
using Contracts.Requests;
using Contracts.Responses;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Persistence.Context;
using Tests.TestsHelpers;

namespace Tests.CQRSTests.Queries;

public class SigninUserQueryTests
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
    public async Task Handler_ExistedUser_ValidAccessToken()
    {
        // Arrange
        const string expectedToken = "valid token";
        var user = await _context.Users.FirstAsync();
        var query = new SigninUserQuery
        {
            Request = new SigninRequest
            {
                Email = user.Email,
                Password = "P@ssw0rd"
            }
        };
        var handler = new SigninUserQuery.SigninQueryHandler(_context, _passwordHasher, _authenticationService);
        // Act
        var token = await handler.Handle(query, CancellationToken.None);
        // Assert
        Assert.That(token.AccessToken, Is.EqualTo(expectedToken));
    }

    [Test]
    public async Task Handler_UserWithIncorrectPassword_ThrowsNotFoundException()
    {
        // Arrange
        var user = await _context.Users.FirstAsync();
        var query = new SigninUserQuery
        {
            Request = new SigninRequest
            {
                Email = user.Email,
                Password = "P@ssw0rd!"
            }
        };
        var handler = new SigninUserQuery.SigninQueryHandler(_context, _passwordHasher, _authenticationService);
        // Act, Assert
        Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
    }

    [Test]
    public void Handler_UserWithIncorrectEmail_ThrowsNotFoundException()
    {
        // Arrange
        var query = new SigninUserQuery
        {
            Request = new SigninRequest
            {
                Email = "!@ d@@fake.com",
                Password = "P@ssw0rd"
            }
        };
        var handler = new SigninUserQuery.SigninQueryHandler(_context, _passwordHasher, _authenticationService);
        // Act, Assert
        Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
    }

    [OneTimeTearDown]
    public async Task DisposeContext()
    {
        await _context.DisposeAsync();
    }
}