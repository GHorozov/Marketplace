using System;

namespace Marketplace.App.ViewModels.Orders
{
    public class MyOrderViewModel
    {
        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public int Quantity { get; set; }
    }
}
