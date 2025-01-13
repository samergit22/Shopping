using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Contracts.Cart;
using Shopping.Contracts.CartItem;
using Shopping.IServices;

namespace Shopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Create a new cart
        [HttpPost("Create/{userId}")]
        public async Task<ActionResult<CartResponse>> CreateCartAsync(string userId)
        {
            var cart = await _cartService.CreateCartAsync(userId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        // Add item to cart
        [HttpPost("AddItem/{cartId}")]
        public async Task<ActionResult<CartResponse>> AddItemToCartAsync(int cartId, [FromBody] CartItemRequest cartItemRequest)
        {
            var cart = await _cartService.AddItemToCartAsync(cartId, cartItemRequest);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        // Remove item from cart
        [HttpDelete("RemoveItem/{cartId}/{cartItemId}")]
        public async Task<ActionResult<CartResponse>> RemoveItemFromCartAsync(int cartId, int cartItemId)
        {
            var cart = await _cartService.RemoveItemFromCartAsync(cartId, cartItemId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        // Get cart by user ID
        [HttpGet("GetByUser/{userId}")]
        public async Task<ActionResult<CartResponse>> GetCartByUserIdAsync(string userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }
    }
}
