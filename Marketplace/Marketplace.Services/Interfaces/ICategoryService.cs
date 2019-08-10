using Marketplace.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Services.Interfaces
{
    public interface ICategoryService
    {
        IQueryable<TModel> GetAllCategories<TModel>();

        Task<Category> GetCategoryById(string id);

        Task<Category> GetCategoryByName(string name);

        Task<bool> Create(string name);

        Task<bool> Edit(string id, string name);
    }
}
