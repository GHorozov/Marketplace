using Marketplace.App.Infrastructure;
using Marketplace.Domain;
using System;
using System.Collections.Generic;

namespace Marketplace.App.ViewModels.Products
{
    public class AllProductViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }

        public DateTime PublishDate { get; set; }

        public bool IsNew => DateTime.UtcNow.Subtract(this.PublishDate).Days < GlobalConstants.LessThanDaysIsNew; 
    }
}
