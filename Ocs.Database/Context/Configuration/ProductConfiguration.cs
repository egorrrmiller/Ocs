using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ocs.Domain.Models;

namespace Ocs.Database.Context.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(Enumerable.Range(0, 50)
            .Select(product => new Product()
            {
                Id = Guid.NewGuid(),
                Qty = 1000
            }));
    }
}