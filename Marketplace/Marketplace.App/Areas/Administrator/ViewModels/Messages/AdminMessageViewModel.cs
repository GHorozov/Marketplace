using System;

namespace Marketplace.App.Areas.Administrator.ViewModels.Messages
{
    public class AdminMessageViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Message { get; set; }

        public DateTime IssuedOn { get; set; }
    }
}
