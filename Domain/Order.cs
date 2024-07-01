
namespace Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid OrderItemId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }

        public void Update(
            Guid orderItemId,
            DateTime orderDate)
        {
            OrderItemId = orderItemId;
            OrderDate = orderDate;
        }
    }
}
