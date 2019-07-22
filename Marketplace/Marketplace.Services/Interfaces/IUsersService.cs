using Marketplace.Domain;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Services.Interfaces
{
    public interface IUserService
    {
        IQueryable<TModel> GetAllUsers<TModel>();

        Task DeleteById(string id);

        Task<MarketplaceUser> GetUserById(string id);

        Task<IEnumerable<string>> GetUserRoles(string id);

        Task<IdentityResult> ChangePassword(string id, string password);
    }
}
