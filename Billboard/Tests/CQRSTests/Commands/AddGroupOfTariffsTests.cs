using Application.CQRS.Commands;
using Contracts.Requests;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Persistence.Context;
using Tests.TestsHelpers;

namespace Tests.CQRSTests.Commands;

public class AddGroupOfTariffsTests
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
    public async Task Handle_ValidGroupOfTariffs_SuccessfullyAdded()
    {
        var request = new AddGroupOfTariffsRequest
        {
            Name = "Test",
            TariffsId =await _context.Tariffs.Select(e => e.Id).Take(1).ToListAsync()
        };
        var command = new AddGroupOfTariffsCommand
        {
            Request = request
        };
        var handler = new AddGroupOfTariffsCommand.AddGroupOfTariffsCommandHandler(_context);
        var response = await handler.Handle(command, CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(response.Name, Is.EqualTo(request.Name));
            Assert.That(response.Tariffs.Select(e =>e.Id), Is.EqualTo(request.TariffsId));
        });
    }
   
    [OneTimeTearDown]
    public async Task DisposeContext()
    {
        await _context.DisposeAsync();
    }
}