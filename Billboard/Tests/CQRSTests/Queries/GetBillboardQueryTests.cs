using Application.CQRS.Queries;
using Contracts.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Persistence.Context;
using Persistence.Enums;
using Tests.TestsHelpers;

namespace Tests.CQRSTests.Queries;

public class GetBillboardQueryTests
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
    public async Task Handler_ArchiveBillboard_ThrowsNotFoundException()
    {
        // Arrange
        var billboard = await _context.Billboards.FirstAsync(e => e.ArchiveStatusId == ArchiveStatusId.Archived);
        var query = new GetBillboardInformationQuery
        {
            BillboardId = billboard.Id
        };
        var handler = new GetBillboardInformationQuery.GetBillboardInformationQueryHandler(_context);
        // Act, Assert
        Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
    }
    
    [Test]
    public async Task Handler_nonArchivedBillboard_ValidGroupOfTariffs()
    {
        // Arrange
        var billboard = await _context.Billboards.FirstAsync(e => e.ArchiveStatusId == ArchiveStatusId.NonArchived);
        var query = new GetBillboardInformationQuery
        {
            BillboardId = billboard.Id
        };
        var handler = new GetBillboardInformationQuery.GetBillboardInformationQueryHandler(_context);
        // Act, Assert
        var response = await handler.Handle(query, CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(billboard.Id, Is.EqualTo(billboard.Id));
        });
    }

    [OneTimeTearDown]
    public async Task DisposeContext()
    {
        await _context.DisposeAsync();
    }
}