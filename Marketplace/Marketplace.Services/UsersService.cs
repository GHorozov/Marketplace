using AutoMapper;
using AutoMapper.QueryableExtensions;
using Marketplace.Data;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Services
{
    public class UsersService : IUserService
    {
        private readonly MarketplaceDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<MarketplaceUser> userManager;

        public UsersService(MarketplaceDbContext context, IMapper mapper, UserManager<MarketplaceUser> userManager)
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

        public async Task<TModel> GetUserById<TModel>(string id)
        {
            var user = await this.userManager
                .FindByIdAsync(id);

            var userDto = this.mapper.Map<TModel>(user);

            return userDto;
        }
    }
}
