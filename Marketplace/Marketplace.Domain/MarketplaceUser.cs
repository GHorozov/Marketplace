using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class MarketplaceUser : IdentityUser
    {
        public MarketplaceUser()
        {
            this.Products = new List<Product>();
            this.Orders = new List<Order>();
            this.WishProducts = new List<WishProduct>();
            this.Messages = new List<Message>();
            this.Comments = new List<Comment>();
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }


        public virtual List<Product> Products { get; set; }

        public virtual List<Order> Orders { get; set; }

        public virtual List<WishProduct> WishProducts { get; set; }

        public virtual List<Message> Messages { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}
