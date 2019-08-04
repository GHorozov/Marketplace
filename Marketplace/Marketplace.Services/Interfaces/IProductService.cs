using Marketplace.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddProduct(Product product);

        Task<bool> AddPicturePath(string productId, string picturePath);

        Task<bool> EditPicturePath(string productId, string url);

        Product GetProductById(string id);

        Task<bool> AddCategory(string productId, Category category);

        IQueryable<TModel> GetAllProducts<TModel>();

        Task<Product> EditProduct(Product product);

        IQueryable<TModel> GetMyProducts<TModel>(string userId);
    }
}
