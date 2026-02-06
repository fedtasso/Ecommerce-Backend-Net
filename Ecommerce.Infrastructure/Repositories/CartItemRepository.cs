using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories;

public class CartItemRepository : ICartItemRepository
{
    private readonly EcommerceDbContext _context;

    public CartItemRepository(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<CartItem?> GetByCartAndProductAsync(long cartId, long productId)
    {
        return await _context.CartItems
            .AsNoTracking()
            .FirstOrDefaultAsync(ci =>
                ci.CartId == cartId &&
                ci.ProductId == productId);
    }

    public async Task AddAsync(CartItem item)
    {
        await _context.CartItems.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CartItem item)
    {
        _context.CartItems.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByCartIdAsync(long cartId)
    {
        await _context.CartItems
            .Where(ci => ci.CartId == cartId)
            .ExecuteDeleteAsync();   // EF Core 7+
    }
}
