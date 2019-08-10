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
    public class OrderService : IOrderService
    {
        private readonly MarketplaceDbContext context;
        private readonly IMapper mapper;

        public OrderService(MarketplaceDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> Create(MarketplaceUser user, string phone, string shippingAddress)
        {
            if (user == null) return false;

            var cart = await this.context
                .ShoppingCarts
                .Where(x => x.UserId == user.Id)
                .Include(x => x.Products)
                .SingleOrDefaultAsync();

            foreach (var product in cart.Products)
            {
                var order = new Order()
                {
                    IssuedOn = DateTime.UtcNow,
                    MarketplaceUserId = user.Id,
                    Quantity = product.Quantity,
                    Phone = phone,
                    ShippingAddress = shippingAddress,
                };

                this.context.Orders.Add(order);
                await this.context.SaveChangesAsync();

                var productOrder = new ProductOrder()
                {
                    OrderId = order.Id,
                    ProductId = product.ProductId
                };

                this.context.ProductOrder.Add(productOrder);
                await this.context.SaveChangesAsync();

                var productFromDb = await this.context
                    .Products
                    .Where(x => x.Id == product.ProductId)
                    .SingleOrDefaultAsync();

                productFromDb.Quantity -= product.Quantity;
                this.context.Products.Update(productFromDb);
                await this.context.SaveChangesAsync();
            }

            cart.Products.Clear();
            this.context.ShoppingCarts.Update(cart);
            await this.context.SaveChangesAsync();

            return true;
        }

        public IQueryable<TModel> GetAllOrders<TModel>()
        {
            var result = this.context
                .Orders
                .Include(x => x.MarketplaceUser)
                .ProjectTo<TModel>(mapper.ConfigurationProvider);

            return result;
        }

        public IQueryable<TModel> GetMyOrders<TModel>(string userId)
        {
            var result = this.context
                .Users
                .Where(x => x.Id == userId)
                .Select(x => x.Orders)
                .ProjectTo<TModel>(mapper.ConfigurationProvider);

            return result;
        }
    }
}
