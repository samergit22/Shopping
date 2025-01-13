namespace Shopping.Contracts.Order
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<OrderItemResponse> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
