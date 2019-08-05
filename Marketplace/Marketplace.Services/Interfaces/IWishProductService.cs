using Marketplace.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Services.Interfaces
{
    public interface IWishProductService
    {
        int GetAllProductsCount(MarketplaceUser user);

        IQueryable<TModel> GetAllWishProducts<TModel>(MarketplaceUser user);

        Task<bool> ClearAll(MarketplaceUser user);

        Task<bool> Detele(MarketplaceUser user, string id);

        Task<bool> Add(MarketplaceUser user, string id);
    }
}
