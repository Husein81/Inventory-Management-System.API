namespace Application.Requests
{
    public class OrderItemRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Qty { get; set; }
        public decimal Discount { get; set; } = 0;
        public Guid ProductId { get; set; }
    }
}
