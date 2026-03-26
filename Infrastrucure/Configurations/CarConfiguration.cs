using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastrucure.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Model)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(c => c.PricePerDay)
        .IsRequired();

        builder.Property(c => c.IsAvialable)
        .HasDefaultValue(true);
    }
}
