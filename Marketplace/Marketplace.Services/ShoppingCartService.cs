using AutoMapper;
using AutoMapper.QueryableExtensions;
using Marketplace.Data;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly MarketplaceDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<MarketplaceUser> userManager;
        private readonly IUserService userService;

        public ShoppingCartService(MarketplaceDbContext context, IMapper mapper, UserManager<MarketplaceUser> userManager, IUserService userService)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            this.userService = userService;
        }

        public async Task<bool> AddProductToShoppingCartAsync(string productId, string username, int quantity)
        {
            var product = this.context
                .Products
                .SingleOrDefault(x => x.Id == productId);

            var user = await this.userService.GetUserByUsername(username);

            if (product == null || quantity <= 0 || user == null) return false;

            var shoppingCartProduct = new ShoppingCartProduct()
            {
                Product = product,
                ShoppingCartId = user.ShoppingCartId,
                Quantity = quantity
            };

            this.context.ShoppingCartProduct.Add(shoppingCartProduct);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<TModel> GetAllShoppingCartProducts<TModel>(MarketplaceUser user)
        {
            var products = this.context
                .ShoppingCartProduct
                .Where(x => x.ShoppingCartId == user.ShoppingCartId)
                .Include(x => x.Product);


            var result = products.ProjectTo<TModel>(mapper.ConfigurationProvider);

            return result;
        }

        public async Task<bool> Delete(string id, string userId)
        {
            if (id == null || userId == null) return false;

            var cart = this.context
                .ShoppingCarts
                .Include(x => x.Products)
                .Where(x => x.UserId == userId)
                .SingleOrDefault();

            var product = cart.Products
                .Where(x => x.ProductId == id)
                .Single();

            cart.Products.Remove(product);
            this.context.ShoppingCarts.Update(cart);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> ClearCart(MarketplaceUser user)
        {
            if (user == null) return false;

            var cart = await this.context
                .ShoppingCarts
                .Where(x => x.User == user)
                .Include(x => x.Products)
                .SingleOrDefaultAsync();

            cart.Products.Clear();

            this.context.ShoppingCarts.Update(cart);
            this.context.Users.Update(user);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> IsCartAny(MarketplaceUser user)
        {
            var cart = await this.context
                .ShoppingCarts
                .Where(x => x.UserId == user.Id)
                .Include(x => x.Products)
                .SingleOrDefaultAsync();

            return cart.Products.Count > 0;
        }
    }
}
