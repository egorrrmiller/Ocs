using Microsoft.EntityFrameworkCore;
using Ocs.Domain.Models;
using Ocs.Infrastructure.Context.Configuration;

namespace Ocs.Infrastructure.Context;

public class OcsContext : DbContext
{
    public OcsContext(DbContextOptions<OcsContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Line> Line { get; set; }

    public DbSet<OrderLines> OrderLines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new LineConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}