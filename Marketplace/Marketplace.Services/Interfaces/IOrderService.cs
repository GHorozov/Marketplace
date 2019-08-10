using Marketplace.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Services.Interfaces
{
    public interface IOrderService
    {
        Task<bool> Create(MarketplaceUser user, string phone, string shippingAddress);

        IQueryable<TModel> GetAllOrders<TModel>();

        IQueryable<TModel> GetMyOrders<TModel>(string userId);
    }
}
