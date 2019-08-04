using System.ComponentModel.DataAnnotations;

namespace Marketplace.App.Areas.Administrator.ViewModels.Categories
{
    public class EditCategoryInputModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Category name")]
        public string Name { get; set; }
    }
}
