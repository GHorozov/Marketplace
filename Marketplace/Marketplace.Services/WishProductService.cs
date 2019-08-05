using AutoMapper;
using AutoMapper.QueryableExtensions;
using Marketplace.Data;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Services
{
    public class WishProductService : IWishProductService
    {
        private readonly MarketplaceDbContext context;
        private readonly IMapper mapper;
        private readonly IProductService productService;
        private readonly IUserService userService;

        public WishProductService(MarketplaceDbContext context, IMapper mapper, IProductService productService, IUserService userService)
        {
            this.context = context;
            this.mapper = mapper;
            this.productService = productService;
            this.userService = userService;
        }

        public int GetAllProductsCount(MarketplaceUser user)
        {
            if (user != null)
            {
                var productCount = user.WishProducts.Count;

                return productCount;
            }

            return 0;
        }

        public IQueryable<TModel> GetAllWishProducts<TModel>(MarketplaceUser user)
        {
            var products = this.context
                .WishProducts
                .Include(x => x.Product)
                .Include(x => x.MarketplaceUser)
                .Where(x => x.MarketplaceUserId == user.Id)
                .Select(x => x.Product)
                .ProjectTo<TModel>(mapper.ConfigurationProvider);

            return products;
        }

        public async Task<bool> Add(MarketplaceUser user, string id)
        {
            if (user == null || id == null) return false;

            var userFromDb = await this.userService.GetUserById(user.Id);

            var isExist = userFromDb.WishProducts.Select(x => x.ProductId).Contains(id);
            if (isExist) return true;

            var wishProduct = new WishProduct()
            {
                MarketplaceUserId = userFromDb.Id,
                MarketplaceUser = userFromDb,
                ProductId = id
            };

            this.context.WishProducts.Add(wishProduct);
            userFromDb.WishProducts.Add(wishProduct);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Detele(MarketplaceUser user, string id)
        {
            if (user == null || id == null) return false;

            var userFromDb = await this.userService.GetUserById(user.Id);
            var wishProduct = await this.context
                .WishProducts
                .Where(x => x.MarketplaceUserId == userFromDb.Id && x.ProductId == id)
                .SingleOrDefaultAsync();

            userFromDb.WishProducts.Remove(wishProduct);
            this.context.Users.Update(userFromDb);
            this.context.WishProducts.Remove(wishProduct);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> ClearAll(MarketplaceUser user)
        {
            if (user == null) return false;

            var userFromDb = await this.userService.GetUserById(user.Id);
            if(userFromDb.WishProducts.Count == 0)
            {
                return true;
            }

            userFromDb.WishProducts.Clear();
            this.context.Users.Update(userFromDb);

            var allWishProducts = this.context
                .WishProducts
                .Where(x => x.MarketplaceUserId == userFromDb.Id).ToList();

            foreach (var item in allWishProducts)
            {
                this.context.WishProducts.Remove(item);
            }

            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }
    }
}
