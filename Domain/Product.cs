using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; }

        public void CalculatePrice()
        {
            decimal PriceBeforeTax = Cost - (Cost * Discount/100);
            Price = PriceBeforeTax + (PriceBeforeTax * Tax/100);
        }
        public void Update(Product product)
        {
            Name = product.Name ;
            Description = product.Description; 
            Cost = product.Cost; 
            Tax = product.Tax;
            Discount = product.Discount;
            ImageUrl = product.ImageUrl;
            CategoryId = product.CategoryId;
            InventoryId = product.InventoryId;
        }
    }
}
