using Marketplace.App.Infrastructure;
using System;

namespace Marketplace.App.ViewModels.Home
{
    public class HomeSearchViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }

        public DateTime PublishDate { get; set; }

        public bool IsNew => DateTime.UtcNow.Subtract(this.PublishDate).Days < GlobalConstants.LessThanDaysIsNew;
    }
}
