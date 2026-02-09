using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> entity)
    {
        entity.ToTable("carts");

        entity.HasKey(c => c.Id);

        entity.Property(c => c.Id)
              .ValueGeneratedOnAdd();

        entity.Property(c => c.TotalPrice)
              .HasColumnType("decimal(10,2)")
              .IsRequired();

        entity.Property(c => c.TotalItems)
              .IsRequired();

        entity.Property(c => c.CreatedAt)
              .HasDefaultValueSql("now()")
              .IsRequired();

        entity.Property(c => c.UpdatedAt)
              .HasDefaultValueSql("now()")
              .IsRequired();

        // Relación 1 a 1 con User
        entity.Property(c => c.UserId)
              .HasColumnName("user_id")
              .IsRequired();

        entity.Ignore(c => c.Items);  //TODO estudiar y revisar esta configuracion

        entity.HasOne(c => c.User)
              .WithOne()
              .HasForeignKey<Cart>(c => c.UserId)              
              .IsRequired();

        // Relación 1 a muchos con CartItem
        entity.HasMany<CartItem>("_items")
              .WithOne(ci => ci.Cart)
              .HasForeignKey(ci => ci.CartId)
              .IsRequired();
    }
}
