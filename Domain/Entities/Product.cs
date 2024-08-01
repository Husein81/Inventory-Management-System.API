namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
       
        public void Update(Product product)
        {
            Name = product.Name;
            Description = product.Description;
            Cost = product.Cost;
            Quantity = product.Quantity;
            Price = product.Price;
            Currency = product.Currency;
            ImageUrl = product.ImageUrl;
            CategoryId = product.CategoryId;
            SupplierId = product.SupplierId;
        }
    }
}
