namespace Application.Requests
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public List<string> ImageUrls { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SupplierId { get; set; }
    }
}
