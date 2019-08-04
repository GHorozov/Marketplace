﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Marketplace.Data;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Services
{
    public class ProductService : IProductService
    {
        private readonly MarketplaceDbContext context;
        private readonly IMapper mapper;

        public ProductService(MarketplaceDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> AddProduct(Product product)
        {
            var result = this.context
                .Products
                .Add(product);

            var isSaved = await this.context.SaveChangesAsync();

            return isSaved > 0;
        }

        public async Task<bool> EditPicturePath(string productId, string url)
        {
            var product = this.GetProductById(productId);
            if (product == null)
            {
                return false;
            }

            var picture = new Picture() { PictureUrl = url };
            var currentPicture = product.Pictures.First();
            product.Pictures.Remove(currentPicture);
            product.Pictures.Add(picture);

            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> AddPicturePath(string productId, string url)
        {
            var product = this.GetProductById(productId);
            if (product == null)
            {
                return false;
            }

            var picture = new Picture() { PictureUrl = url };
            product.Pictures.Add(picture);

            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public Product GetProductById(string id)
        {
            var product = this.context
                .Products
                .Include(x => x.Category)
                .Include(x => x.Pictures)
                .Include(x => x.Comments)
                .Include(x => x.Ratings)
                .Where(x => x.Id == id)
                .SingleOrDefault();

            return product;
        }

        public async Task<bool> AddCategory(string productId, Category category)
        {
            var product = this.GetProductById(productId);
            if (product == null)
            {
                return false;
            }

            product.Category = category;

            this.context.Update(product);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<TModel> GetAllProducts<TModel>()
        {
            var products = this.context
                .Products
               .Include(x => x.Category)
               .Include(x => x.Pictures)
               .Include(x => x.ShoppingCarts);
               

            var result = products.ProjectTo<TModel>(mapper.ConfigurationProvider);

            return result;
        }

        public async Task<Product> EditProduct(Product product)
        {
            this.context.Update(product);
            var result = await this.context.SaveChangesAsync();

            return product;
        }

        public IQueryable<TModel> GetMyProducts<TModel>(string userId)
        {
            var products = this.context
                 .Products
                 .Include(x => x.Category)
                 .Include(x => x.Pictures)
                 .Where(x => x.MarketplaceUserId == userId);

            var result = products.ProjectTo<TModel>(mapper.ConfigurationProvider);

            return result;
        }
    }
}