using Mapster;
using Microsoft.EntityFrameworkCore;
using Shopping.Contracts.Cart;
using Shopping.Contracts.CartItem;
using Shopping.Data;
using Shopping.IServices;
using Shopping.Models;

namespace Shopping.Services
{
    public class CartService : ICartService
    {
        private readonly ShoppingData _data;

        public CartService(ShoppingData data)
        {
            _data = data;
        }

        public async Task<CartResponse> CreateCartAsync(string userId)
        {
            var user = await _data.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var cart = new Cart
            {
                UserId = userId // Set the UserId to associate this cart with the user
            };

            var existingCart = await _data.Carts.FirstOrDefaultAsync(c => c.UserId == userId);

            if (existingCart != null)
            {
                return existingCart.Adapt<CartResponse>(); // Return existing cart if it already exists
            }

            _data.Carts.Add(cart);
            await _data.SaveChangesAsync();

            // Map Cart to CartResponse using Mapster
            return cart.Adapt<CartResponse>();
        }

        public async Task<CartResponse> AddItemToCartAsync(int cartId, CartItemRequest cartItemRequest)
        {
            var cart = await _data.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == cartId);
            if (cart == null)
            {
                throw new Exception("Cart not found.");  // 404 Not Found would be better in a real API
            }
            var product = await _data.Products.FindAsync(cartItemRequest.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found.");  // 404 Not Found would be better in a real API
            }

            var existingCartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == cartItemRequest.ProductId);
            if (existingCartItem != null)
            {
                // Update the quantity of the existing cart item
                existingCartItem.Quantity += cartItemRequest.Quantity;
            }
            else
            {
                
                var cartItem = cartItemRequest.Adapt<CartItem>();
                cartItem.CartId = cartId; // Set the cart ID manually since it's not part of cartItemRequest
                cartItem.Price = product.Price; // Set the product price manually

                cart.Items.Add(cartItem);  // Add the new item to the cart
            }

            await _data.SaveChangesAsync();

            // Map Cart to CartResponse after adding item
            return cart.Adapt<CartResponse>();
        }

        public async Task<CartResponse> RemoveItemFromCartAsync(int cartId, int cartItemId)
        {
            var cartItem = await _data.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                throw new Exception("not founded");
            }

            _data.CartItems.Remove(cartItem);
            await _data.SaveChangesAsync();

            var cart = await _data.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == cartId);

            // Map Cart to CartResponse after removing item
            return cart.Adapt<CartResponse>();
        }

        public async Task<CartResponse> GetCartByUserIdAsync(string userId)
        {
            var cart = await _data.Carts.Include(c => c.Items)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            // Map Cart to CartResponse with its cart items
            return cart.Adapt<CartResponse>();
        }
    }
}
