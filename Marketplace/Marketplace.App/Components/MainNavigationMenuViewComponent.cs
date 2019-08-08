using Marketplace.App.ViewModels.Components;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.Components
{
    [ViewComponent]
    public class MainNavigationMenuViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public MainNavigationMenuViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var allCategories = this.categoryService.GetAllCategories<IndexCategoryViewModel>().ToList();
            var resultModel = new MainNavigationMenuViewModel()
            {
                Categories = allCategories
            };

            return View(resultModel);
        }
    }
}
