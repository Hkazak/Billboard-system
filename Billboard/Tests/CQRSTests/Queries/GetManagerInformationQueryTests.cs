using Application.CQRS.Queries;
using Contracts.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Persistence.Context;
using Persistence.Enums;
using Tests.TestsHelpers;

namespace Tests.CQRSTests.Queries;

public class GetManagerInformationQueryTests
{
    private BillboardContext _context = default!;

    [OneTimeSetUp]
    public async Task SetupContext()
    {
        var options = new DbContextOptionsBuilder<BillboardContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new BillboardContext(options);
        await _context.SeedManagersAsync();
    }

    [Test]
    public async Task Handler_InactiveManager_ThrowsNotFoundException()
    {
        // Arrange
        var manager = await _context.Managers.FirstAsync(e => e.StatusId == ManagerStatusId.Inactive);
        var query = new GetManagerInformationQuery
        {
            ManagerId = manager.Id
        };
        var handler = new GetManagerInformationQuery.GetUserInformationQueryHandler(_context);
        // Act, Assert
        Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
    }
    
    [Test]
    public async Task Handler_ActiveManager_ValidManager()
    {
        // Arrange
        var manager = await _context.Managers.FirstAsync(e => e.StatusId == ManagerStatusId.Active);
        var query = new GetManagerInformationQuery
        {
            ManagerId = manager.Id
        };
        var handler = new GetManagerInformationQuery.GetUserInformationQueryHandler(_context);
        // Act, Assert
        var response = await handler.Handle(query, CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(manager.Id, Is.EqualTo(response.Id));
            Assert.That(manager.Email, Is.EqualTo(response.Email));
            Assert.That(manager.FirstName, Is.EqualTo(response.FirstName));
            Assert.That(manager.LastName, Is.EqualTo(response.LastName));
            Assert.That(manager.MiddleName, Is.EqualTo(response.MiddleName));
            Assert.That(manager.Phone, Is.EqualTo(response.Phone));
        });
    }

    [OneTimeTearDown]
    public async Task DisposeContext()
    {
        await _context.DisposeAsync();
    }
}