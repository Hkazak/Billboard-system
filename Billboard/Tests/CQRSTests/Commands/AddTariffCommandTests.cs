using Application.CQRS.Commands;
using Contracts.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Persistence.Context;
using Tests.TestsHelpers;

namespace Tests.CQRSTests.Commands;

public class AddTariffCommandTests
{
    private BillboardContext _context = default! ;

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
    public async Task Handle_ValidTariff_SuccessfullyAdded()
    {
        var request = new AddTariff
        {
            Title = "Test",
            StartTime = TimeSpan.FromHours(5),
            EndTime = TimeSpan.FromHours(6),
            Price = 7845,
        };
        var command = new AddTariffCommand
        {
            Request = request
        };
        var handler = new AddTariffCommand.AddTariffCommandHandler(_context);
        var response = await handler.Handle(command, CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(response.Title, Is.EqualTo(request.Title));
            Assert.That(response.StartTime, Is.EqualTo(request.StartTime.ToString(@"hh\:mm")));
            Assert.That(response.EndTime, Is.EqualTo(request.EndTime.ToString(@"hh\:mm")));
            Assert.That(response.Price, Is.EqualTo(request.Price));
        });
    }
   
    [OneTimeTearDown]
    public async Task DisposeContext()
    {
        await _context.DisposeAsync();
    }
}