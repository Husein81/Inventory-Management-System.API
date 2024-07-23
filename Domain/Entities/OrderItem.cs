namespace Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public void CalculateTotalPrice()
        {
            UnitPrice = Product.Price;
            Price = Quantity * UnitPrice;
        }

        public void Update(OrderItem orderItem)
        {
            ProductId = orderItem.ProductId;
            OrderId = orderItem.OrderId;
            Quantity = orderItem.Quantity;
            UnitPrice = orderItem.UnitPrice;
            Price = orderItem.Price;
        }

    }
}
