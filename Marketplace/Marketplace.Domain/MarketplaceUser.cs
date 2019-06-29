using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual List<Product> Products { get; set; }

        public virtual List<Order> Orders { get; set; }

        public virtual List<WishProduct> WishProducts { get; set; }

        public virtual List<Message> Messages { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}
