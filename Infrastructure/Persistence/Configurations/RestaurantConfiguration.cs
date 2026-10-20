namespace DeliveryApp.Infrastructure.Persistence.Configurations;

using DeliveryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(r => r.Address)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(r => r.PhoneNumber)
                 .IsRequired()
                 .HasMaxLength(20);

        builder.HasMany(r => r.Orders)
            .WithOne(o => o.Restaurant)
            .HasForeignKey(o => o.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}