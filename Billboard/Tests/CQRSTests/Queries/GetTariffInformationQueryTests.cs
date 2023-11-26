using Application.CQRS.Queries;
using Contracts.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Persistence.Context;
using Persistence.Enums;
using Tests.TestsHelpers;

namespace Tests.CQRSTests.Queries;

public class GetTariffInformationQueryTests
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
    public async Task Handler_ArchivedTariff_ThrowsNotFoundException()
    {
        // Arrange
        var tariff = await _context.Tariffs.FirstAsync(e => e.ArchiveStatusId == ArchiveStatusId.Archived);
        var query = new GetTariffInformationQuery
        {
            TariffId = tariff.Id
        };
        var handler = new GetTariffInformationQuery.GetTariffInformationQueryHandler(_context);
        // Act, Assert
        Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
    }
    
    [Test]
    public async Task Handler_nonArchivedTariff_ValidTariff()
    {
        // Arrange
        var tariff = await _context.Tariffs.FirstAsync(e => e.ArchiveStatusId == ArchiveStatusId.NonArchived);
        var query = new GetTariffInformationQuery
        {
            TariffId = tariff.Id
        };
        var handler = new GetTariffInformationQuery.GetTariffInformationQueryHandler(_context);
        // Act, Assert
        var response = await handler.Handle(query, CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(tariff.Id, Is.EqualTo(response.Id));
            Assert.That(tariff.Price, Is.EqualTo(response.Price));
            Assert.That(tariff.StartTime.ToString(@"hh\:mm"), Is.EqualTo(response.StartTime));
            Assert.That(tariff.EndTime.ToString(@"hh\:mm"), Is.EqualTo(response.EndTime));
        });
    }

    [OneTimeTearDown]
    public async Task DisposeContext()
    {
        await _context.DisposeAsync();
    }
}