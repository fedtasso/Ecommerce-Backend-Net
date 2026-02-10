using Ecommerce.Application.DTOs.Cart;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Mappers;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly CartMapper _cartMapper;

    public CartService(
        ICartRepository cartRepository,
        ICartItemRepository cartItemRepository,
        IUserRepository userRepository,
        IProductRepository productRepository,
        CartMapper cartMapper)
    {
        _cartRepository = cartRepository;
        _cartItemRepository = cartItemRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
        _cartMapper = cartMapper;
    }

    public async Task<CartResponse> GetCartByUserIdAsync(long userId)
    {
        var cart = await _cartRepository.GetByUserIdWithItemsAsync(userId)
                   ?? throw new ActiveCartNotFoundException(userId);

        return _cartMapper.ToResponse(cart);
    }

    public async Task<CartResponse> AddItemToCartAsync(long userId, AddToCartRequest request)
    {
        var user = await GetUserOrThrowAsync(userId);
        var product = await GetProductOrThrowAsync(request.ProductId);

        ValidateStock(product, request.Quantity);

        var cart = await GetOrCreateCartAsync(user);

        var existingItem = await _cartItemRepository
            .GetByCartAndProductAsync(cart.Id, product.Id);

        if (existingItem != null)
        {
            existingItem.IncreaseQuantity(request.Quantity);
            await _cartItemRepository.UpdateAsync(existingItem);
        }
        else
        {
            var newItem = new CartItem(cart, product, request.Quantity);
            cart.AddItem(newItem);
            await _cartItemRepository.AddAsync(newItem);
        }

        await _cartRepository.UpdateAsync(cart);

        return _cartMapper.ToResponse(cart);
    }

    public async Task<CartResponse> UpdateCartItemAsync(long userId, AddToCartRequest request)
    {
        var cart = await _cartRepository.GetByUserIdWithItemsAsync(userId)
                   ?? throw new ActiveCartNotFoundException(userId);

        var product = await GetProductOrThrowAsync(request.ProductId);

        ValidateStock(product, request.Quantity);

        var cartItem = await _cartItemRepository
            .GetByCartAndProductAsync(cart.Id, product.Id)
            ?? throw new ProductNotInCartException(product.Id);

        cartItem.SetQuantity(request.Quantity);
        await _cartItemRepository.UpdateAsync(cartItem);

        await _cartRepository.UpdateAsync(cart);

        return _cartMapper.ToResponse(cart);
    }

    public async Task<CartResponse> RemoveItemFromCartAsync(long userId, long productId)
    {
        var cart = await _cartRepository.GetByUserIdWithItemsAsync(userId)
                   ?? throw new ActiveCartNotFoundException(userId);

        var cartItem = await _cartItemRepository
            .GetByCartAndProductAsync(cart.Id, productId)
            ?? throw new ProductNotInCartException(productId);

        cart.RemoveItem(cartItem);

        await _cartItemRepository.DeleteByCartIdAsync(cartItem.Id);
        await _cartRepository.UpdateAsync(cart);

        return _cartMapper.ToResponse(cart);
    }

    public async Task<CartResponse> ClearCartAsync(long userId)
    {
        var cart = await _cartRepository.GetByUserIdWithItemsAsync(userId)
                   ?? throw new ActiveCartNotFoundException(userId);

        await _cartItemRepository.DeleteByCartIdAsync(cart.Id);
        cart.Clear();

        await _cartRepository.UpdateAsync(cart);

        return _cartMapper.ToResponse(cart);
    }

    public async Task DeleteCartByUserIdAsync(long userId)
    {
        var cart = await _cartRepository.GetByUserIdAsync(userId)
                   ?? throw new ActiveCartNotFoundException(userId);

        await _cartRepository.DeleteAsync(cart);
    }


    private async Task<User> GetUserOrThrowAsync(long userId)
    {
        return await _userRepository.GetByIdAsync(userId)
               ?? throw new UserNotFoundException(userId);
    }

    private async Task<Product> GetProductOrThrowAsync(long productId)
    {
        return await _productRepository.GetByIdAsync(productId)
               ?? throw new ProductNotFoundException(productId);
    }

    private async Task<Cart> GetOrCreateCartAsync(User user)
    {
        var cart = await _cartRepository.GetByUserAsync(user);

        if (cart != null)
            return cart;

        var newCart = new Cart(user);
        await _cartRepository.AddAsync(newCart);

        return newCart;
    }

    private static void ValidateStock(Product product, int quantity)
    {
        if (product.Stock < quantity)
            throw new InsufficientStockException(
                product.Title,
                quantity,
                product.Stock
                );
    }
}
