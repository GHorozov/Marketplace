using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class ShoppingCartProduct
    {
        public string ShoppingCartId { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }


        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
