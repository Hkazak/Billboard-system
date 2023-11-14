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
    
    public required DbSet<User> Users { get; set; }
    public required DbSet<UserRole> UserRoles { get; set; }
    public required DbSet<Manager> Managers { get; set; }
    public required DbSet<ManagerStatus> ManagerStatus { get; set; }
    public required DbSet<Tariff> Tariffs { get; set; }
    public required DbSet<Billboard> Billboards { get; set; }
    public required DbSet<BillboardSurface> BillboardSurfaces { get; set; }
    public required DbSet<GroupOfTariffs> GroupOfTariffs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new ManagerConfiguration());
        modelBuilder.ApplyConfiguration(new ManagerStatusConfiguration());
        modelBuilder.ApplyConfiguration(new TariffConfiguration());
        modelBuilder.ApplyConfiguration(new BillboardConfiguration());
        modelBuilder.ApplyConfiguration(new GroupOfTariffsConfiguration());
    }
}