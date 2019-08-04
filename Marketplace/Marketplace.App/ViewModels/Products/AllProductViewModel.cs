using Marketplace.Domain;
using System.Collections.Generic;

namespace Marketplace.App.ViewModels.Products
{
    public class AllProductViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }
    }
}
