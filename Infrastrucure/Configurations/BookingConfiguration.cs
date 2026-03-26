using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastrucure.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.StartDate)
        .IsRequired();

        builder.Property(b => b.EndDate)
       .IsRequired();

       builder.HasOne(b => b.User)
       .WithMany(u => u.Bookings)
       .HasForeignKey(b => b.UserId)
       .OnDelete(DeleteBehavior.Cascade);

       builder.HasOne(b => b.Car)
       .WithMany(c => c.Bookings)
       .HasForeignKey(b => b.CarId)
       .OnDelete(DeleteBehavior.Cascade);
    }
}
