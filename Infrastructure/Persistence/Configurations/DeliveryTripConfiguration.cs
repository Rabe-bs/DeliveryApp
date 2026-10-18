using DeliveryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class DeliveryTripConfiguration : IEntityTypeConfiguration<DeliveryTrip>
    {
        public void Configure(EntityTypeBuilder<DeliveryTrip> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(t => t.CurrentLatitude)
                .HasColumnType("decimal(18,6)")
                .IsRequired() ;

            builder.Property(t => t.CurrentLongitude)
                .HasColumnType("decimal(18,6)")
                .IsRequired() ;

            builder.Property(t =>t.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.IsCompleted)
                .IsRequired();

            builder.HasOne(t => t.Order)
                .WithMany()
                .HasForeignKey(t => t.OrderId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }

}
