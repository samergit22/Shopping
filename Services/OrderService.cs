using Mapster;
using Microsoft.EntityFrameworkCore;
using Shopping.Contracts.Order;
using Shopping.Data;
using Shopping.IServices;
using Shopping.Models;

namespace Shopping.Services
{
    public class OrderService : IOrderService
    {
        private readonly ShoppingData _data;

        public OrderService(ShoppingData data)
        {
            _data = data;
        }

        public async Task<OrderResponse> CreateOrderAsync(OrderRequest orderRequest)
        {
            var user = await _data.Users.FindAsync(orderRequest.UserId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var order = orderRequest.Adapt<Order>();  
            foreach (var item in orderRequest.Items)
            {
                var product = await _data.Products.FindAsync(item.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {item.ProductId} not found.");
                }

                var orderItem = item.Adapt<OrderItem>(); 
                orderItem.Price = product.Price;

                order.Items.Add(orderItem);  
            }
            _data.Orders.Add(order);  // Add the new order to the database
            await _data.SaveChangesAsync();
            return order.Adapt<OrderResponse>();
        }

        public async Task<OrderResponse> GetOrderByIdAsync(int orderId)
        {
            var order = await _data.Orders
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            // Use Mapster to convert Order entity to OrderResponse
            return order.Adapt<OrderResponse>();
        }

        public async Task<List<OrderResponse>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await _data.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            // Use Mapster to convert List of Order entities to List of OrderResponse
            return orders.Adapt<List<OrderResponse>>();
        }
    }
}
