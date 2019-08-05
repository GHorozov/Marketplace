using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Marketplace.Services
{
    public class PictureService : IPictureService
    {
        private readonly IProductService productService;

        public PictureService(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<string> SavePicture(string productId, IFormFile picture, string defaultPicturesPath)
        {
            var product =await this.productService.GetProductById(productId);

            var path = defaultPicturesPath + $"{product.PublishDate.Year}-{picture.FileName}";

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await picture.CopyToAsync(stream);
            }

            path = path.Replace("wwwroot", "");
            return path;
        }
    }
}
