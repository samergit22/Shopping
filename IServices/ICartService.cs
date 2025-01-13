using Shopping.Contracts.Cart;
using Shopping.Contracts.CartItem;

namespace Shopping.IServices
{
    public interface ICartService
    {
        Task<CartResponse> CreateCartAsync(string userId);
        Task<CartResponse> AddItemToCartAsync(int cartId, CartItemRequest cartItemRequest);
        Task<CartResponse> RemoveItemFromCartAsync(int cartId, int cartItemId);
        Task<CartResponse> GetCartByUserIdAsync(string userId);
    }
}
