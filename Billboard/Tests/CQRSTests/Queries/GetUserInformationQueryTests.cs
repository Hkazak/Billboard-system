using Application.CQRS.Queries;
using Contracts.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Persistence.Context;
using Tests.TestsHelpers;

namespace Tests.CQRSTests.Queries;

public class GetUserInformationQueryTests
{
    private BillboardContext _context = default!;

    [OneTimeSetUp]
    public async Task SetupContext()
    {
        var options = new DbContextOptionsBuilder<BillboardContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new BillboardContext(options);
        await _context.SeedUsersAsync();
    }

    [Test]
    public async Task Handler_NotExistedUser_ThrowsNotFoundException()
    {
        // Arrange
        var query = new GetUserInformationQuery
        {
            UserId = Guid.Empty
        };
        var handler = new GetUserInformationQuery.GetUserInformationQueryHandler(_context);
        // Act, Assert
        Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
    }
    
    [Test]
    public async Task Handler_ExistedUser_ValidUser()
    {
        // Arrange
        var user = await _context.Users.FirstAsync();
        var query = new GetUserInformationQuery
        {
            UserId = user.Id
        };
        var handler = new GetUserInformationQuery.GetUserInformationQueryHandler(_context);
        // Act, Assert
        var response = await handler.Handle(query, CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(user.Id, Is.EqualTo(response.Id));
            Assert.That(user.Email, Is.EqualTo(response.Email));
            Assert.That(user.Name, Is.EqualTo(response.Name));
        });
    }

    [OneTimeTearDown]
    public async Task DisposeContext()
    {
        await _context.DisposeAsync();
    }
}