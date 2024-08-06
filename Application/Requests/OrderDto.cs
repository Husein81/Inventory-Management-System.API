using Domain.Entities;

namespace Application.Requests
{
    public record OrderDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string OrderStatus { get; set; }
        public string ShippingAddress { get; set; }
        public decimal ItemsPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid CustomerId { get; set; }
        public OrderCustomer Customer { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
