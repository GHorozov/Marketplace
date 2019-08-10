using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.ViewModels.Orders
{
    public class OrderViewModel
    {
        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public int ProductsCount { get; set; }

        public decimal OrderTotal { get; set; }
    }
}
