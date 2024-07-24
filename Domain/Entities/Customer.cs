﻿
namespace Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public List<Order> Orders { get; set; }

        public  void Update(Customer customer)
        {
            Name = customer.Name;
            Email = customer.Email;
            Address = customer.Address;
        }

    }
}