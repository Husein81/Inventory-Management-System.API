namespace Application.Requests.DashboardDto
{
    public record ProductDto
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
