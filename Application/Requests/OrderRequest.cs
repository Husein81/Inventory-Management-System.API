using Domain.Entities;

namespace Application.Requests
{
    public class OrderRequest
    {
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public decimal Payment { get; set; }
        public string ShippingAddress { get; set; }
        public decimal ItemsPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderItemRequest> OrderItems { get; set; }

    }
    public class CreateOrderRequest
    {
        public List<OrderItemRequest> OrderItems { get; set; }
        public decimal ItemsPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public record UpdateOrderStatusDto
    {
        public string OrderStatus { get; set; }
    }
    public record UpdateOrderPaymentDto
    {
        public decimal Payment { get; set; }
    }
}
