using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class Category
    {
        public Category()
        {
            this.Products = new List<CategoryProduct>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public virtual List<CategoryProduct> Products { get; set; }
    }
}
