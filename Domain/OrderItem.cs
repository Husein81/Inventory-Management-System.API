namespace Domain
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public void CalculateTotalPrice()
        {
            UnitPrice = Product.Price;
            TotalPrice = Quantity * UnitPrice;
        }

        public void Update(
            Guid productId,
            Guid orderId,
            int quantity)
        {
            ProductId = productId;
            OrderId = orderId;
            Quantity = quantity;
        }

    }
}
