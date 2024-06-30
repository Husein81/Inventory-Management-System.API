﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Requests
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public Guid InventoryId { get; set; }

    }
}
