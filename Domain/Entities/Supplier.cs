﻿namespace Domain.Entities
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public void Update(Supplier supplier)
        {
            Name = supplier.Name;
            Email = supplier.Email;
            Address = supplier.Address;
            Phone = supplier.Phone;
            UpdatedAt = DateTime.Now;
        }
    }
}
