using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public class BillboardContext : DbContext
{
    protected BillboardContext()
    {
    }

    public BillboardContext(DbContextOptions options) : base(options)
    {
    }
}