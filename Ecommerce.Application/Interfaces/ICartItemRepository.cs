using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces;

public interface ICartItemRepository
{
    Task<CartItem?> GetByCartAndProductAsync(long cartId, long productId);

    Task AddAsync(CartItem item);

    Task UpdateAsync(CartItem item);

    Task DeleteByCartIdAsync(long cartId);
}
