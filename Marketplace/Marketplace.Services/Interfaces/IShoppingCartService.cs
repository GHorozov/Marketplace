using Marketplace.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<bool> AddProductToShoppingCartAsync(string productId, string userId, int quantity);

        IQueryable<TModel> GetAllShoppingCartProducts<TModel>(MarketplaceUser user);

        Task<bool> Delete(string id, string userId);

        Task<bool> ClearCart(MarketplaceUser user);

        Task<bool> IsCartAny(MarketplaceUser user);
    }
}
