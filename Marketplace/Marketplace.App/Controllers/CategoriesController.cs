using Marketplace.App.ViewModels.Categories;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.Controllers
{
    public class CategoriesController :Controller
    {
        private readonly IProductService productService;

        public CategoriesController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult Category(string id, string name)
        {
            if(id == null)
            {
                return this.Redirect("/");
            }

            var resultModel = this.productService.GetProductsByCategoryId<CategoriesProductViewModel>(id).ToList();
            this.ViewData["ProductsHead"] = name;

            return this.View(resultModel);
        }
    }
}
