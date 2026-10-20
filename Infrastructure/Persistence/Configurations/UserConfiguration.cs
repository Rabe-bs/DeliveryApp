namespace DeliveryApp.Infrastructure.Persistence.Configurations;

using DeliveryApp.Domain.Entities;
using global::DeliveryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(u => u.Email)
            .IsUnique(); // منع تكرار البريد الإلكتروني

        builder.Property(p => p.PhoneNumber)
             .IsRequired()
             .HasMaxLength(15);

        builder.Property(u => u.Role)
            .HasConversion<string>()
            .IsRequired();


        builder.Property(u => u.PasswordHash)
            .IsRequired();

        // علاقة 1 لـ 1 بين المستخدم والمحفظة
        builder.HasOne(u => u.Wallet)
            .WithOne(w => w.User)
            .HasForeignKey<Wallet>(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}