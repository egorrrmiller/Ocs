using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ocs.Domain.Models;

namespace Ocs.Database.Context.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasMany(lines => lines.Lines)
            .WithMany(orders => orders.Orders)
            .UsingEntity<OrderLines>();

        builder.Property(status => status.Status)
            .IsRequired();

        builder.Property(dateTime => dateTime.Created)
            .IsRequired()
            .HasDefaultValue(DateTime.UtcNow);

        builder.Property(deleted => deleted.Deleted)
            .IsRequired()
            .HasDefaultValue(false);
    }
}