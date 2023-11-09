using Microsoft.EntityFrameworkCore;
using Persistence.ModelConfigurations;
using Persistence.Models;

namespace Persistence.Context;

public class BillboardContext : DbContext
{
    protected BillboardContext()
    {
    }

    public BillboardContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; init; } = default!;
    public DbSet<UserRole> UserRoles { get; init; } = default!;
    public DbSet<Manager> Managers { get; init; } = default!;
    public DbSet<ManagerStatus> ManagerStatus { get; init; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new ManagerConfiguration());
        modelBuilder.ApplyConfiguration(new ManagerStatusConfiguration());
    }
}