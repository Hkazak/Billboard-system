using Application.CQRS.Commands;
using Contracts.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Persistence.Context;
using Persistence.Enums;
using Tests.TestsHelpers;

namespace Tests.CQRSTests.Commands;

public class DeleteBillboardCommandTests
{
    private BillboardContext _context = default!;
    
    [OneTimeSetUp]
    public async Task SetupContext()
    {
        var options = new DbContextOptionsBuilder<BillboardContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new BillboardContext(options);
        await _context.SeedBillboardsAsync();
    }
    
    [Test]
    public async Task Handle_ExistedNonArchivedBillboard_ChangeArchiveStatusToArchived()
    {
        var billboard = await _context.Billboards.FirstOrDefaultAsync(e => e.ArchiveStatusId == ArchiveStatusId.NonArchived);
        var command = new DeleteBillboardCommand
        {
            BillboardId = billboard.Id,
        };
        var handler = new DeleteBillboardCommand.DeleteBillboardCommandHandler(_context);
        Assert.DoesNotThrowAsync(async () => await handler.Handle(command, CancellationToken.None));
        Assert.That(billboard.ArchiveStatusId, Is.EqualTo(ArchiveStatusId.Archived));
    }
    
    [Test]
    public async Task Handle_ExistedArchivedBillboard_ThrowsNotFoundException()
    {
        var billboard = await _context.Billboards.FirstAsync(e => e.ArchiveStatusId == ArchiveStatusId.Archived);
        var command = new DeleteBillboardCommand
        {
            BillboardId = billboard.Id
        };
        var handler = new DeleteBillboardCommand.DeleteBillboardCommandHandler(_context);
        Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
    }
    
    [OneTimeTearDown]
    public async Task DisposeContext()
    {
        await _context.DisposeAsync();
    }
}