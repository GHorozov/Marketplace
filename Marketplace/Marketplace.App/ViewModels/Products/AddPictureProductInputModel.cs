using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.App.ViewModels.Products
{
    public class AddPictureProductInputModel
    {
        public string Id { get; set; }

        [Required]
        public IFormFile Picture { get; set; }
    }
}
