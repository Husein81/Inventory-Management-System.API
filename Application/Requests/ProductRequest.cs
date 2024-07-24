namespace Application.Requests
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal Tax { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryRequest Category { get; set; }
        public Guid SupplierId { get; set; }
        public SupplierRequest Supplier { get; set; }
        public List<OrderItemRequest> OrderItems { get; set; }
    }
}
