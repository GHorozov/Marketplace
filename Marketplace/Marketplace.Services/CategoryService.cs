using AutoMapper;
using AutoMapper.QueryableExtensions;
using Marketplace.Data;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly MarketplaceDbContext context;
        private readonly IMapper mapper;

        public CategoryService(MarketplaceDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IQueryable<TModel> GetAllCategories<TModel>()
        {
            var result = this.context
                .Categories
                .ProjectTo<TModel>(mapper.ConfigurationProvider);

            return result;
        }

        public async Task<bool> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;

            var category = new Category() { Name = name };

            this.context.Categories.Add(category);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Category> GetCategoryById(string id)
        {
            var category = await this.context
                .Categories
                .SingleOrDefaultAsync(x => x.Id == id);

            return category;
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            var category = await this.context
                .Categories
                .SingleOrDefaultAsync(x => x.Name == name);

            return category;
        }

        public async Task<bool> Edit(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name)) return false;

            var category = await this.GetCategoryById(id);
            if (category == null) return false;

            category.Name = name;

            this.context.Categories.Update(category);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }
    }
}
