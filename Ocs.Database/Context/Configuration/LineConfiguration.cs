using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ocs.Domain.Models;

namespace Ocs.Database.Context.Configuration;

public class LineConfiguration : IEntityTypeConfiguration<Line>
{
    public void Configure(EntityTypeBuilder<Line> builder) => builder.HasData(Enumerable.Range(0, 50)
        .Select(_ => new Line
        {
            Id = Guid.NewGuid()
        }));
}