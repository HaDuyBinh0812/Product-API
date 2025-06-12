﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Entities
{
    public class Products : BasicEntity<int>
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }

        public string ProductPicture { set; get; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}