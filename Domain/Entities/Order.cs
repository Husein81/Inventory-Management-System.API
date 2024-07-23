namespace Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Invoice Invoice { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
        public Guid CustomerId { get; set; }
        public Customer Customer {  get; set; }

        public void Update(Order order)
        {
            OrderDate = order.OrderDate;
            OrderStatus = order.OrderStatus;
            OrderItems = order.OrderItems;
            AppUserId = order.AppUserId;
            Invoice = order.Invoice;
            CustomerId = order.CustomerId;
        }
    }
}
