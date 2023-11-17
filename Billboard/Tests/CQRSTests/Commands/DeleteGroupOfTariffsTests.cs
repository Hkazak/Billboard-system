using Application.CQRS.Commands;
using Contracts.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Persistence.Context;
using Persistence.Enums;
using Tests.TestsHelpers;

namespace Tests.CQRSTests.Commands;

public class DeleteGroupOfTariffsTests
{
    private BillboardContext _context = default! ;

    [OneTimeSetUp]
    public async Task SetupContext()
    {
        var options = new DbContextOptionsBuilder<BillboardContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new BillboardContext(options);
        await _context.SeedGroupOfTariffsAsync();
    }
    
    [Test]
    public async Task Handle_ExistedNonArchivedGroupOfTariffs_ChangeArchiveStatusToArchived()
    {
        var groupOfTariffs = await _context.GroupOfTariffs.FirstAsync(e => e.ArchiveStatusId == ArchiveStatusId.NonArchived);
        var command = new DeleteGroupOfTariffsCommand
        {
            GroupId = groupOfTariffs.Id
        };
        var handler = new DeleteGroupOfTariffsCommand.DeleteGroupOfTariffsCommandHandler(_context);
        Assert.DoesNotThrowAsync(async () => await handler.Handle(command, CancellationToken.None));
        Assert.That(groupOfTariffs.ArchiveStatusId, Is.EqualTo(ArchiveStatusId.Archived));
    }
    
    [Test]
    public async Task Handle_ExistedArchivedGroupOfTariffs_ThrowsNotFoundException()
    {
        var groupOfTariffs = await _context.GroupOfTariffs.FirstAsync(e => e.ArchiveStatusId == ArchiveStatusId.Archived);
        var command = new DeleteGroupOfTariffsCommand
        {
            GroupId = groupOfTariffs.Id
        };
        var handler = new DeleteGroupOfTariffsCommand.DeleteGroupOfTariffsCommandHandler(_context);
        Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
    }
    
    [OneTimeTearDown]
    public async Task DisposeContext()
    {
        await _context.DisposeAsync();
    }
}