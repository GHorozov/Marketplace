using AutoMapper;
using AutoMapper.QueryableExtensions;
using Marketplace.Data;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using System.Linq;

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

        public void Create(string name)
        {
            var category = new Category() { Name = name };

            this.context.Categories.Add(category);
            this.context.SaveChanges();
        }

        public Category GetCategoryById(string id)
        {
            var category = this.context
                .Categories
                .SingleOrDefault(x => x.Id == id);

            return category;
        }

        public Category GetCategoryByName(string name)
        {
            var category = this.context
                .Categories
                .SingleOrDefault(x => x.Name == name);

            return category;
        }

        public void Edit(string id, string name)
        {
            var category = this.GetCategoryById(id);
            if (category == null)
            {
                return;
            }

            category.Name = name;

            this.context.Categories.Update(category);
            this.context.SaveChanges();
        }
    }
}
