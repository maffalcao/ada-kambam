using Kambam.Domain.Entities;
using Kambam.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Kambam.Infra.Context;

public class MyContext : DbContext
{
    public DbSet<CardEntity>? Cards { get; set; }

    public MyContext(DbContextOptions<MyContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CardEntity>(new CardMap().Configure);
    }
}