using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class Comment
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string MarketplaceUserId { get; set; }
        public virtual MarketplaceUser MarketplaceUser { get; set; }
    }
}
