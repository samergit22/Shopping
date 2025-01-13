namespace Shopping.Contracts.Order
{
    public class OrderRequest
    {
        public string UserId { get; set; }  // The user who is placing the order
        public List<OrderItemRequest> Items { get; set; }  // List of items in the order
    }
}
