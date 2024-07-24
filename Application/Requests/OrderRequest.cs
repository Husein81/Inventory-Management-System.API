using Domain.Entities;

namespace Application.Requests
{
    public class OrderRequest
    {
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public InvoiceRequest Invoice { get; set; }
        public List<OrderItemRequest> OrderItems { get; set; } = new();
        public Guid CustomerId { get; set; }
        public CustomerRequest Customer { get; set; }
    }
}
