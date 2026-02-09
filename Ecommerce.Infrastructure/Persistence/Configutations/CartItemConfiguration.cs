using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> entity)
    {
        entity.ToTable("cart_items");

        entity.HasKey(ci => ci.Id);

        entity.Property(ci => ci.Id)
              .ValueGeneratedOnAdd();

        entity.Property(ci => ci.CartId)
              .HasColumnName("cart_id")
              .IsRequired();

        entity.Property(ci => ci.ProductId)
              .HasColumnName("product_id")
              .IsRequired();

      //   entity.HasOne(ci => ci.Cart)
      //         .WithMany(c => c.Items)
      //         .HasForeignKey(ci => ci.CartId);

        entity.HasOne(ci => ci.Cart)
              .WithMany("_items")
              .HasForeignKey(ci => ci.CartId)
              .IsRequired();

        entity.HasOne(ci => ci.Product)
              .WithMany()
              .HasForeignKey(ci => ci.ProductId);

        entity.Property(ci => ci.Quantity)
              .IsRequired();

        entity.Property(ci => ci.UnitPrice)
              .HasPrecision(10, 2)
              .IsRequired();

        entity.Property(ci => ci.Subtotal)
              .HasPrecision(10, 2)
              .IsRequired();

        entity.Property(ci => ci.CreatedAt)
              .IsRequired();

        entity.Property(ci => ci.UpdatedAt)
              .IsRequired();
    }
}
