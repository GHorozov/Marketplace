using AutoMapper;
using AutoMapper.QueryableExtensions;
using Marketplace.Data;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Services
{
    public class UserService : IUserService
    {
        private readonly MarketplaceDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<MarketplaceUser> userManager;

        public UserService(MarketplaceDbContext context, IMapper mapper, UserManager<MarketplaceUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public IQueryable<TModel> GetAllUsers<TModel>()
        {
            var users = this.context
                .Users
                .ProjectTo<TModel>(mapper.ConfigurationProvider);

            return users;
        }

        public async Task<bool> DeleteById(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            if (user != null)
            {
                await this.userManager.DeleteAsync(user);

                return true;
            }

            return false;
        }

        public async Task<MarketplaceUser> GetUserById(string id)
        {
            var user = await this.context
                .Users
                .Where(x => x.Id == id)
                .Include(x => x.ShoppingCart)
                .Include(x => x.Products)
                .Include(x => x.Orders)
                .Include(x => x.WishProducts)
                .Include(x => x.Messages)
                .SingleOrDefaultAsync();

            return user;
        }

        public async Task<IEnumerable<string>> GetUserRoles(string id)
        {
            var user = await this.context
                .Users
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if(user == null) return new List<string>();

            var userInRoles = await this.userManager
                .GetRolesAsync(user);

            return userInRoles;
        }

        public async Task<IdentityResult> ChangePassword(string id, string password)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var token = await this.userManager.GeneratePasswordResetTokenAsync(user);
            var result = await this.userManager.ResetPasswordAsync(user, token, password);

            return result;
        }

        public int GetAllWishProductsCount(MarketplaceUser user)
        {
            if (user == null)
            {
                return 0;
            }

            var count = user.WishProducts.Count();

            return count;
        }

        public async Task<MarketplaceUser> GetUserByUsername(string username)
        {
            return  await this.context
                .Users
                .SingleOrDefaultAsync(x => x.UserName == username);
        }
    }
}
