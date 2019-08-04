using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.ViewModels.ShoppingCart
{
    public class ShoppingCartProductsViewModel
    {
        public List<ShoppingCartViewModel> Products { get; set; }

        public decimal TotalProductsCost => this.Products.Select(x => x.Total).Sum();
    }
}
