using Marketplace.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public List<HomeCategoryViewModel> Categories { get; set; }

        public List<HomeProductViewModel> Products { get; set; }
    }
}
