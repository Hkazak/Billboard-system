using Application.CQRS.Commands;
using Contracts.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Persistence.Context;
using Persistence.Enums;
using Tests.TestsHelpers;

namespace Tests.CQRSTests.Commands;

public class DeleteTariffCommandTests
{
    private BillboardContext _context = default!;
    
    [OneTimeSetUp]
    public async Task SetupContext()
    {
        var options = new DbContextOptionsBuilder<BillboardContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new BillboardContext(options);
        await _context.SeedTariffsAsync();
    }

    [Test]
    public async Task Handle_ExistedNonArchivedTariff_ChangeArchiveStatusToArchived()
    {
        var tariff = await _context.Tariffs.FirstAsync(e => e.ArchiveStatusId == ArchiveStatusId.NonArchived);
        var command = new DeleteTariffCommand
        {
            TariffId = tariff.Id
        };
        var handler = new DeleteTariffCommand.DeleteTariffCommandHandler(_context);
        Assert.DoesNotThrowAsync(async () => await handler.Handle(command, CancellationToken.None));
        Assert.That(tariff.ArchiveStatusId, Is.EqualTo(ArchiveStatusId.Archived));
    }
    
    [Test]
    public async Task Handle_ExistedArchivedTariff_ThrowsNotFoundException()
    {
        var tariff = await _context.Tariffs.FirstAsync(e => e.ArchiveStatusId == ArchiveStatusId.Archived);
        var command = new DeleteTariffCommand
        {
            TariffId = tariff.Id
        };
        var handler = new DeleteTariffCommand.DeleteTariffCommandHandler(_context);
        Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
    }
    
    [OneTimeTearDown]
    public async Task DisposeContext()
    {
        await _context.DisposeAsync();
    }
}