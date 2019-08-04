using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class Order
    {
        public Order()
        {
            this.Products = new List<ProductOrder>();
        }

        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public int Quantity { get; set; }

        public string ShippingAddress { get; set; }

        public string Phone { get; set; }

        public string MarketplaceUserId { get; set; }
        public virtual MarketplaceUser MarketplaceUser { get; set; }

        public virtual List<ProductOrder> Products { get; set; }
    }
}
