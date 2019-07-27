using Marketplace.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marketplace.Services.Interfaces
{
    public interface ICategoryService
    {
        IQueryable<TModel> GetAllCategories<TModel>();

        Category GetCategoryById(string id);

        Category GetCategoryByName(string name);

        void Create(string name);

        void Edit(string id, string name);
    }
}
