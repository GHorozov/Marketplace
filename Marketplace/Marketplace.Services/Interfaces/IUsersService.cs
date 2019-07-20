using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Services.Interfaces
{
    public interface IUserService
    {
        IQueryable<TModel> GetAllUsers<TModel>();

        Task DeleteById(string id);

        Task<TModel> GetUserById<TModel>(string id);
    }
}
