using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests
{
    public class OrderItemRequest
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
