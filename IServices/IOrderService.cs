using Shopping.Contracts.Order;

namespace Shopping.IServices
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateOrderAsync(OrderRequest orderRequest);
        Task<OrderResponse> GetOrderByIdAsync(int orderId);
        Task<List<OrderResponse>> GetOrdersByUserIdAsync(string userId);
    }
}
