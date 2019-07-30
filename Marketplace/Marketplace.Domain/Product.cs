using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Domain
{
    public class Product
    {
        private const string PriceMinValue = "0.00";
        private const string PriceMaxValue = "79228162514264337593543950335";

        public Product()
        {
            this.Orders = new List<ProductOrder>();
            this.Pictures = new List<Picture>();
            this.Ratings = new List<Rating>();
            this.Comments = new List<Comment>();
            this.ShoppingCarts = new List<ShoppingCartProduct>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime PublishDate { get; set; } = DateTime.UtcNow;

        public string Description { get; set; }

        public string Color { get; set; }

        public string MarketplaceUserId { get; set; }
        public virtual MarketplaceUser MarketplaceUser { get; set; }

        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<Picture> Pictures { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<Rating> Ratings { get; set; }

        public virtual List<ProductOrder> Orders { get; set; }

        public virtual List<ShoppingCartProduct> ShoppingCarts { get; set; }
    }
}
