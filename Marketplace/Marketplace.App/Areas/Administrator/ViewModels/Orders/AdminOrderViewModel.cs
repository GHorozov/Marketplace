using System;

namespace Marketplace.App.Areas.Administrator.ViewModels.Orders
{
    public class AdminOrderViewModel
    {
        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public string Email { get; set; }
    }
}
