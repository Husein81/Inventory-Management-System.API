namespace Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Qty { get; set; }
        public decimal Discount { get; set; } = 0;
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public void Update(OrderItem orderItem)
        {
            Name = orderItem.Name;
            ProductId = orderItem.ProductId;
            Qty = orderItem.Qty;
            Price = orderItem.Price;
        }

    }
}
