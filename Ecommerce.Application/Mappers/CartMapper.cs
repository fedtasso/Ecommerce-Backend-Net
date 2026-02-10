using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Mappers;

public class CartMapper
{
    public CartResponse ToResponse(Cart cart)
    {
        return new CartResponse
        {
            Id = cart.Id,
            UserId = cart.User.Id,
            UserName = cart.User.Name,
            Items = cart.Items.Select(ToItemResponse).ToList(),
            TotalItems = cart.Items.Sum(i => i.Quantity),
            TotalPrice = cart.Items.Sum(i => i.Quantity * i.UnitPrice),
            CreatedAt = cart.CreatedAt,
            UpdatedAt = cart.UpdatedAt
        };
    }

    public CartItemResponse ToItemResponse(CartItem cartItem)
    {
        return new CartItemResponse
        {
            Id = cartItem.Id,
            ProductId = cartItem.Product.Id,
            ProductTitle = cartItem.Product.Title,
            ProductPrice = cartItem.Product.Price,
            ProductImage = cartItem.Product.Image,
            Quantity = cartItem.Quantity,
            UnitPrice = cartItem.UnitPrice,
            Subtotal = cartItem.Quantity * cartItem.UnitPrice
        };
    }
}
