using Marketplace.App.Infrastructure;
using Marketplace.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.ViewModels.Home
{
    public class HomeProductViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public DateTime PublishDate { get; set; }

        public bool IsNew => DateTime.UtcNow.Subtract(this.PublishDate).Days < GlobalConstants.LessThanDaysIsNew;
    }
}
