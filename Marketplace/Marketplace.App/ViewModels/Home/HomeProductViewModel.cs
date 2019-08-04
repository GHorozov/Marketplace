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
    }
}
