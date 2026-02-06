using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private readonly EcommerceDbContext _context;

    public CartRepository(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<Cart?> GetByIdAsync(long id)
    {
        return await _context.Carts
                             .AsNoTracking()
                             .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Cart>> GetAllAsync()
    {
        return await _context.Carts
                             .AsNoTracking()
                             .ToListAsync();
    }

    public async Task AddAsync(Cart cart)
    {
        await _context.Carts.AddAsync(cart);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Cart cart)
    {
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Cart cart)
    {
        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync();
    }
    public async Task<Cart?> GetByUserAsync(User user)
    {
        return await _context.Carts
                             .AsNoTracking()
                             .FirstOrDefaultAsync(c => c.UserId == user.Id);
    }

    public async Task<Cart?> GetByUserIdAsync(long userId)
    {
        return await _context.Carts
                             .AsNoTracking()
                             .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<Cart?> GetByUserIdWithItemsAsync(long userId)
    {
        return await _context.Carts
                             .Include(c => c.Items)   // JOIN FETCH
                             .AsNoTracking()
                             .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<bool> ExistsByUserIdAsync(long userId)
    {
        return await _context.Carts
                             .AnyAsync(c => c.UserId == userId);
    }
}
