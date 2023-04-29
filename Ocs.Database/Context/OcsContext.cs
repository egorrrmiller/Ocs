using Microsoft.EntityFrameworkCore;
using Ocs.Database.Context.Configuration;
using Ocs.Domain.Models;

namespace Ocs.Database.Context;

public class OcsContext : DbContext
{
    public OcsContext(DbContextOptions<OcsContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<OrderProduct> OrderProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}