using Shopping.Contracts.CartItem;

namespace Shopping.Contracts.Cart
{
    public class CartResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IEnumerable<CartItemResponse> CartItems { get; set; } = new List<CartItemResponse>();
    }
}
