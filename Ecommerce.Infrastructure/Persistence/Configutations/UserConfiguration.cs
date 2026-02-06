using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.ToTable("users");

        entity.HasKey(u => u.Id);

        entity.Property(u => u.Id)
              .ValueGeneratedOnAdd();

        entity.Property(u => u.Name)
              .IsRequired();

        entity.Property(u => u.Email)
              .IsRequired();

        entity.HasIndex(u => u.Email)
              .IsUnique();

        entity.Property(u => u.Password)
              .IsRequired()
              .HasMaxLength(255);

        entity.Property(u => u.Role)
              .HasConversion<string>()
              .IsRequired();

        entity.Property(u => u.Active)
              .HasDefaultValue(true);
    }
}