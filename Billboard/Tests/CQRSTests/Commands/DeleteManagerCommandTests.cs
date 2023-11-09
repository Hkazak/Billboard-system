using Application.CQRS.Commands;
using Contracts.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Persistence.Context;
using Persistence.Enums;
using Tests.TestsHelpers;

namespace Tests.CQRSTests.Commands;

public class DeleteManagerCommandTests
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
    public async Task Handle_ExistedActiveManager_ChangeStatusToInactive()
    {
        var manager = await _context.Managers.FirstAsync(e=>e.StatusId==ManagerStatusId.Active);
        var command = new DeleteManagerCommand
        {
            ManagerId = manager.Id
        };
        var handler = new DeleteManagerCommand.DeleteManagerCommandHandler(_context);
        Assert.DoesNotThrowAsync(async () => await handler.Handle(command, CancellationToken.None));
        Assert.That(manager.StatusId, Is.EqualTo(ManagerStatusId.Inactive));
    }
    
    [Test]
    public async Task Handle_ExistedInactiveManager_ThrowsNotFoundException()
    {
        var manager = await _context.Managers.FirstAsync(e=>e.StatusId==ManagerStatusId.Inactive);
        var command = new DeleteManagerCommand
        {
            ManagerId = manager.Id
        };
        var handler = new DeleteManagerCommand.DeleteManagerCommandHandler(_context);
        Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
    }

    [OneTimeTearDown]
    public async Task DisposeContext()
    {
        await _context.DisposeAsync();
    }
}