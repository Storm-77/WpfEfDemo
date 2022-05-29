﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SqlDemo.Models
{
    public partial class Product
    {
        public Product()
        {
            Sales = new HashSet<Sale>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
