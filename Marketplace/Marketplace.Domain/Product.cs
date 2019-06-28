using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class Product
    {
        public Product()
        {
            this.Orders = new List<ProductOrder>();
            this.Categories = new List<CategoryProduct>();
            this.Pictures = new List<Picture>();
            this.Ratings = new List<Rating>();
            this.Comments = new List<Comment>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Brand { get; set; }

        public string Description { get; set; }

        public string TechnicalSpecifications { get; set; }

        public virtual List<Rating> Ratings { get; set; }

        public string MarketplaceUserId { get; set; }
        public virtual MarketplaceUser MarketplaceUser { get; set; }

        public virtual List<ProductOrder> Orders { get; set; }

        public virtual List<CategoryProduct> Categories { get; set; }

        public virtual List<Picture> Pictures { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}
