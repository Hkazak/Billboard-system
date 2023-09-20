﻿using Microsoft.EntityFrameworkCore;
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
}