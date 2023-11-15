using Bogus;
using Persistence.Context;
using Persistence.Enums;
using Persistence.Models;

namespace Tests.TestsHelpers;

public static class DataSeeder
{
    public static async Task SeedUsersAsync(this BillboardContext context)
    {
        var userFaker = new Faker<User>();
        userFaker.RuleFor(e => e.Email, faker => faker.Person.Email)
            .RuleFor(e => e.Password, "b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342")
            .RuleFor(e => e.Name, faker => faker.Person.FirstName)
            .RuleFor(e => e.Id, faker => faker.Random.Guid())
            .RuleFor(e => e.RoleId, UserRoleId.Client);
        await context.Users.AddRangeAsync(userFaker.Generate(10));
        await context.SaveChangesAsync();
    }
    
    public static async Task SeedManagersAsync(this BillboardContext context)
    {
        var activeManagerFaker = new Faker<Manager>();
        var inactiveManagerFaker = new Faker<Manager>();
        activeManagerFaker.RuleFor(e => e.Email, faker => faker.Person.Email)
            .RuleFor(e => e.Password, "b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342")
            .RuleFor(e => e.FirstName, faker => faker.Person.FirstName)
            .RuleFor(e => e.MiddleName, faker => faker.Person.UserName)
            .RuleFor(e => e.LastName, faker => faker.Person.LastName)
            .RuleFor(e => e.Phone, faker => faker.Person.Phone)
            .RuleFor(e=>e.StatusId, ManagerStatusId.Active)
            .RuleFor(e => e.Id, faker => faker.Random.Guid());
        inactiveManagerFaker.RuleFor(e => e.Email, faker => faker.Person.Email)
            .RuleFor(e => e.Password, "b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342")
            .RuleFor(e => e.FirstName, faker => faker.Person.FirstName)
            .RuleFor(e => e.MiddleName, faker => faker.Person.UserName)
            .RuleFor(e => e.LastName, faker => faker.Person.LastName)
            .RuleFor(e => e.Phone, faker => faker.Person.Phone)
            .RuleFor(e=>e.StatusId, ManagerStatusId.Inactive)
            .RuleFor(e => e.Id, faker => faker.Random.Guid());
        await context.Managers.AddRangeAsync(activeManagerFaker.Generate(10));
        await context.Managers.AddRangeAsync(inactiveManagerFaker.Generate(5));
        await context.SaveChangesAsync();
    }

    public static async Task SeedTariffsAsync(this BillboardContext context)
    {
        await context.Tariffs.AddRangeAsync(GenerateTariffs(5, ArchiveStatusId.Archived));
        await context.Tariffs.AddRangeAsync(GenerateTariffs(5, ArchiveStatusId.NonArchived));

        await context.SaveChangesAsync();
    }

    public static async Task SeedGroupOfTariffsAsync(this BillboardContext context)
    {
        var archivedGroupOfTariffs = new Faker<GroupOfTariffs>();
        var nonArchivedGroupOfTariffs = new Faker<GroupOfTariffs>();
        archivedGroupOfTariffs.RuleFor(e => e.Id, faker => faker.Random.Guid())
            .RuleFor(e => e.Name, faker => faker.Name.JobTitle())
            .RuleFor(e => e.ArchiveStatusId, ArchiveStatusId.Archived)
            .RuleFor(e => e.Tariffs, GenerateTariffs(5, ArchiveStatusId.Archived));
        nonArchivedGroupOfTariffs.RuleFor(e => e.Id, faker => faker.Random.Guid())
            .RuleFor(e => e.Name, faker => faker.Name.JobTitle())
            .RuleFor(e => e.ArchiveStatusId, ArchiveStatusId.NonArchived)
            .RuleFor(e => e.Tariffs, GenerateTariffs(10, ArchiveStatusId.NonArchived));
        await context.GroupOfTariffs.AddRangeAsync(archivedGroupOfTariffs.Generate(5));
        await context.GroupOfTariffs.AddRangeAsync(nonArchivedGroupOfTariffs.Generate(10));
        await context.SaveChangesAsync();
    }

    private static List<Tariff> GenerateTariffs(int count, ArchiveStatusId statusId)
    {
        var tariffsFaker = new Faker<Tariff>();
        tariffsFaker.RuleFor(e => e.Id, faker => faker.Random.Guid())
            .RuleFor(e => e.Title, faker => faker.Name.JobTitle())
            .RuleFor(e => e.StartTime, faker => faker.Date.Timespan(TimeSpan.FromHours(12)))
            .RuleFor(e => e.EndTime, faker => faker.Date.Timespan(TimeSpan.FromHours(12)))
            .RuleFor(e => e.ArchiveStatusId,  statusId);

        return tariffsFaker.Generate(count);
    }
}