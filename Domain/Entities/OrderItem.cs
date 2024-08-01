namespace Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public void Update(OrderItem orderItem)
        {
            Name = orderItem.Name;
            ProductId = orderItem.ProductId;
            Quantity = orderItem.Quantity;
            Price = orderItem.Price;
        }

    }
}
