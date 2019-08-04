using Marketplace.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.ViewModels.Products
{
    public class DetailsProductViewModel
    {
        public string Id { get; set; }

        public string CategoryName { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }

        public bool IsMyProduct { get; set; }

        public virtual List<Picture> Pictures { get; set; }
    }
}
