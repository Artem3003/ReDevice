using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReDevice.Database.Entities;

namespace ReDevice.Database.Configurations;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.ToTable("devices");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.SerialNumber)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(d => d.SerialNumber)
            .IsUnique();

        builder.Property(d => d.Category)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Condition)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.Price)
            .HasPrecision(18, 2);

        builder.Property(d => d.IsAvailable)
            .HasDefaultValue(true);

        builder.Property(d => d.CreatedAtUtc)
            .IsRequired();
    }
}

