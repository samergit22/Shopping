using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Contracts.Order;
using Shopping.IServices;

namespace Shopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderRequest orderRequest)
        {
            try
            {
                var orderResponse = await _orderService.CreateOrderAsync(orderRequest);
                return Ok(orderResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderByIdAsync(int orderId)
        {
            try
            {
                var orderResponse = await _orderService.GetOrderByIdAsync(orderId);
                return Ok(orderResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetOrdersByUserIdAsync(string userId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByUserIdAsync(userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
