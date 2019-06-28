using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class Message
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        public string MarketplaceUserId { get; set; }
        public virtual MarketplaceUser MarketplaceUser { get; set; }
    }
}
