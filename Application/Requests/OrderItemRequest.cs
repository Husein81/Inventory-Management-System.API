namespace Application.Requests
{
    public class OrderItemRequest
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public OrderRequest Order { get; set; }
        public ProductRequest Product { get; set; }
    }
}
