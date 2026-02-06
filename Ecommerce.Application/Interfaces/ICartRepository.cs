using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetByIdAsync(long id);

    Task<IEnumerable<Cart>> GetAllAsync();

    Task AddAsync(Cart cart);

    Task UpdateAsync(Cart cart);

    Task DeleteAsync(Cart cart);

    Task<Cart?> GetByUserAsync(User user);

    Task<Cart?> GetByUserIdAsync(long userId);

    Task<Cart?> GetByUserIdWithItemsAsync(long userId);

    Task<bool> ExistsByUserIdAsync(long userId);
}
