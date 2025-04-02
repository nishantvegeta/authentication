using System;
using Microsoft.EntityFrameworkCore;
using auth.Entity;

namespace auth.Data;

public class FirstRunDbContext : DbContext
{
    public FirstRunDbContext(DbContextOptions<FirstRunDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}
