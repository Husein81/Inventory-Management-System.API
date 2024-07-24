namespace Application.Requests
{
    public class CategoryRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<ProductRequest> Products { get; set; }
    }
}
