using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class Message
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string MessageContent { get; set; }

        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        public string MarketplaceUserId { get; set; }
        public virtual MarketplaceUser MarketplaceUser { get; set; }
    }
}
