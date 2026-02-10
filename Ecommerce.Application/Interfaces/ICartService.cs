using Ecommerce.Application.DTOs.Cart;

namespace Ecommerce.Application.Interfaces;

public interface ICartService
{
    Task<CartResponse> GetCartByUserIdAsync(long userId);

    Task<CartResponse> AddItemToCartAsync(long userId, AddToCartRequest request);

    Task<CartResponse> UpdateCartItemAsync(long userId, AddToCartRequest request);

    Task<CartResponse> RemoveItemFromCartAsync(long userId, long productId);

    Task<CartResponse> ClearCartAsync(long userId);

    Task DeleteCartByUserIdAsync(long userId);
}
