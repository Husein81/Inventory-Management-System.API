
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<Order> Orders { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public  void Update(Customer customer)
        {
            Name = customer.Name;
            Email = customer.Email;
            Phone = customer.Phone;
            Address = customer.Address;
            UpdatedAt = DateTime.Now;
        }

    }
}
