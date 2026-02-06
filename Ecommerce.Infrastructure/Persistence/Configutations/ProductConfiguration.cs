using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.ToTable("products");

        entity.HasKey(p => p.Id);

        entity.Property(p => p.Id)
              .ValueGeneratedOnAdd();

        entity.Property(p => p.Title)
              .IsRequired()
              .HasMaxLength(255);

        entity.Property(p => p.Price)
              .IsRequired()
              .HasColumnType("decimal(10,2)");

        entity.Property(p => p.Description)
              .HasColumnType("text");

        entity.Property(p => p.Category)
              .IsRequired()
              .HasMaxLength(100);

        entity.Property(p => p.Image)
              .HasMaxLength(500);

        entity.Property(p => p.Stock)
              .IsRequired();

        entity.Property(p => p.CreatedAt)
              .HasDefaultValueSql("now()");
    }
}
