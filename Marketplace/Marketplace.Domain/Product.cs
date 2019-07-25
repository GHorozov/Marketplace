using System;
using System.Collections.Generic;

namespace Marketplace.Domain
{
    public class Product
    {
        public Product()
        {
            this.Orders = new List<ProductOrder>();
            this.Categories = new List<CategoryProduct>();
            this.Colors = new List<Color>();
            this.Pictures = new List<Picture>();
            this.Ratings = new List<Rating>();
            this.Comments = new List<Comment>();
            this.ShoppingCarts = new List<ShoppingCartProduct>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime PublishDate { get; set; } = DateTime.UtcNow;

        public string Description { get; set; }

        public string MarketplaceUserId { get; set; }
        public virtual MarketplaceUser MarketplaceUser { get; set; }

        public virtual List<Color> Colors { get; set; }

        public virtual List<Picture> Pictures { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<Rating> Ratings { get; set; }

        public virtual List<ProductOrder> Orders { get; set; }

        public virtual List<CategoryProduct> Categories { get; set; }

        public virtual List<ShoppingCartProduct> ShoppingCarts { get; set; }
    }
}
