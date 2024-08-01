namespace Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string ShippingAddress { get; set; }
        public decimal ItemsPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer {  get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public void Update(Order order)
        {
            OrderDate = order.OrderDate;
            OrderStatus = order.OrderStatus;
            ShippingAddress = order.ShippingAddress;
            ItemsPrice = order.ItemsPrice;
            Discount = order.Discount;
            TotalAmount = order.TotalAmount;
            CustomerId = order.CustomerId;
            OrderItems = order.OrderItems;
        }
    }
}
