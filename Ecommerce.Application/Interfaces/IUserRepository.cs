using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(long id);

    Task<IEnumerable<User>> GetAllAsync();

    Task AddAsync(User user);

    Task UpdateAsync(User user);

    Task DeleteAsync(User user);

    Task<bool> ExistsByEmailAsync(string email);

    Task<User?> GetByEmailAsync(string email);
}
