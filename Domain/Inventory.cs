﻿namespace Domain
{
    public class Inventory
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int StockLevel { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
