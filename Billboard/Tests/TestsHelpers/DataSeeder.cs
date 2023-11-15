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
}