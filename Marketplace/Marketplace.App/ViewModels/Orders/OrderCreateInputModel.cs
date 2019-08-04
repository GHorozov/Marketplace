using System.ComponentModel.DataAnnotations;

namespace Marketplace.App.ViewModels.Orders
{
    public class OrderCreateInputModel
    {
        private const int AddressMinLenght = 10;
        private const int AddressMaxLenght = 120;

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [StringLength(AddressMaxLenght, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = AddressMinLenght)]
        public string ShippingAddress { get; set; }
    }
}
