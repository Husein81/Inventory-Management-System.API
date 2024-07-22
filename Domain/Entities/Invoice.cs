namespace Domain.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public DateTime LastUpdated { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public void Update(Invoice invoice)
        {
            LastUpdated = invoice.LastUpdated;
            TotalAmount = invoice.TotalAmount;
            OrderId = invoice.OrderId;
            Order = invoice.Order;
        }
    }
}
