using Marketplace.App.ViewModels.Components;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public SideBarViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var allCategories = this.categoryService.GetAllCategories<SideBarCategoryViewModel>().ToList();
            var resultModel = new SideBarViewModel() { Categories = allCategories };

            return this.View(resultModel);
        }
    }
}
