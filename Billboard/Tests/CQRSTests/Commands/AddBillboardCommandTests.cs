using Application.CQRS.Commands;
using Application.Extensions;
using Contracts.Requests;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Persistence.Context;
using Persistence.Enums;
using Persistence.Models;
using Tests.TestsHelpers;

namespace Tests.CQRSTests.Commands;

public class AddBillboardCommandTests
{
    private BillboardContext _context = default! ;

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
    public async Task Handle_ValidBillboard_SuccessfullyAdded()
    {
        var surface = await _context.BillboardSurfaces.FirstOrDefaultAsync();
        var groupOfTariffs = await _context.GroupOfTariffs.FirstOrDefaultAsync();
        var request = new AddBillboardRequest
        {
            Name = "Test",
            Address = "Test",
            Description = "Test",
            GroupOfTariffs = groupOfTariffs!.Id,
            BillboardType = BillboardTypeId.TripleSide.ToString(),
            BillboardSurfaceId = surface!.Id,
            Height = 0,
            Width = 0
        };
        var command = new AddBillboardCommand
        {
            Request = request
        };
        var handler = new AddBillboardCommand.AddBillboardCommandHandler(_context);
        var response = await handler.Handle(command, CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(response.Name, Is.EqualTo(request.Name));
            Assert.That(response.Address, Is.EqualTo(request.Address));
            Assert.That(response.Description, Is.EqualTo(request.Description));
            Assert.That(response.BillboardType, Is.EqualTo(request.BillboardType));
            Assert.That(response.Height, Is.EqualTo(request.Height));
            Assert.That(response.Width, Is.EqualTo(request.Width));
            Assert.That(response.BillboardSurface, Is.EqualTo(surface.Surface));
            Assert.That(response.Penalty, Is.EqualTo(request.Penalty));
        });
    }
   
    [OneTimeTearDown]
    public async Task DisposeContext()
    {
        await _context.DisposeAsync();
    }
}