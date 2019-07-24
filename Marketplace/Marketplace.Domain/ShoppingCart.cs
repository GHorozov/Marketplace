using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            this.Products = new List<ShoppingCartProduct>();
        }

        public string Id { get; set; }

        public string UserId { get; set; }
        public MarketplaceUser User { get; set; }

        public virtual List<ShoppingCartProduct> Products { get; set; }
    }
}
