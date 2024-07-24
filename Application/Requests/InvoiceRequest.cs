namespace Application.Requests
{
    public class InvoiceRequest
    {
        public DateTime LastUpdated { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid OrderId { get; set; }
        public OrderRequest Order { get; set; }
    }
}
