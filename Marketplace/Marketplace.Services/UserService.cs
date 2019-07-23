using AutoMapper;
using AutoMapper.QueryableExtensions;
using Marketplace.Data;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            this.userManager = userManager;
        }
       
        public IQueryable<TModel> GetAllUsers<TModel>()
        {
            var users = this.context
                .Users
                .ProjectTo<TModel>(mapper.ConfigurationProvider);

            return users;
        }

        public async Task DeleteById(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            if(user != null)
            {
                await this.userManager.DeleteAsync(user);
            }
        }

        public async Task<MarketplaceUser> GetUserById(string id)
        {
            var user = await this.userManager
                .FindByIdAsync(id);

            return user;
        }

        public async Task<IEnumerable<string>> GetUserRoles(string id)
        {
            var user = await this.userManager
                .FindByIdAsync(id);

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
    }
}
