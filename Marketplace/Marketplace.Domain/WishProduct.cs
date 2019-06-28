using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class WishProduct
    {
        public string MarketplaceUserId { get; set; }
        public MarketplaceUser MarketplaceUser { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
