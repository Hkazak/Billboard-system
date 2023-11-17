using Application.CQRS.Queries;
using Contracts.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Persistence.Context;
using Persistence.Enums;
using Tests.TestsHelpers;

namespace Tests.CQRSTests.Queries;

public class GetGroupOfTariffsInformationQueryTests
{
    private BillboardContext _context = default!;

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
    public async Task Handler_ArchiveGroupOfTariffs_ThrowsNotFoundException()
    {
        // Arrange
        var groupOfTariffs = await _context.GroupOfTariffs.FirstAsync(e => e.ArchiveStatusId == ArchiveStatusId.Archived);
        var query = new GetGroupOfTariffsQuery
        {
            Id = groupOfTariffs.Id
        };
        var handler = new GetGroupOfTariffsQuery.GetGroupOfTariffsQueryHandler(_context);
        // Act, Assert
        Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
    }
    
    [Test]
    public async Task Handler_nonArchivedGroupOfTariffs_ValidGroupOfTariffs()
    {
        // Arrange
        var groupOfTariffs = await _context.GroupOfTariffs.FirstAsync(e => e.ArchiveStatusId == ArchiveStatusId.NonArchived);
        var query = new GetGroupOfTariffsQuery
        {
            Id = groupOfTariffs.Id
        };
        var handler = new GetGroupOfTariffsQuery.GetGroupOfTariffsQueryHandler(_context);
        // Act, Assert
        var response = await handler.Handle(query, CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(groupOfTariffs.Id, Is.EqualTo(response.Id));
            Assert.That(response.Tariffs, Is.EqualTo(response.Tariffs));
        });
    }

    [OneTimeTearDown]
    public async Task DisposeContext()
    {
        await _context.DisposeAsync();
    }
}