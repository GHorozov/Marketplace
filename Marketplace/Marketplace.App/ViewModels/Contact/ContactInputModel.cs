using Marketplace.App.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.App.ViewModels.Contact
{
    public class ContactInputModel
    {
        [Required]
        [StringLength(GlobalConstants.ContactUserNameMaxLenght, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = GlobalConstants.ContactUserNameMinLenght)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [StringLength(GlobalConstants.ContactMessageMaxLenght, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = GlobalConstants.ContactMessageMinLenght)]
        public string Message { get; set; }
    }
}
