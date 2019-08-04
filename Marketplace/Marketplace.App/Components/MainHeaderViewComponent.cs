using AutoMapper;
using Marketplace.App.ViewModels.Components;
using Marketplace.App.ViewModels.ShoppingCart;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.Components
{
    public class MainHeaderViewComponent : ViewComponent
    {
        private readonly UserManager<MarketplaceUser> userManager;
        private readonly ICategoryService categoryService;
        private readonly IWishProductService wishProductService;
        private readonly IShoppingCartService shoppingCartService;

        public MainHeaderViewComponent(UserManager<MarketplaceUser> userManager, ICategoryService categoryService, IWishProductService wishProductService, IShoppingCartService shoppingCartService)
        {
            this.userManager = userManager;
            this.categoryService = categoryService;
            this.wishProductService = wishProductService;
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var allCategories = this.categoryService.GetAllCategories<IndexCategoryViewModel>().ToList();
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                var wishProductsCount = this.wishProductService.GetAllProductsCount(user);
                var shoppingCartProducts = this.shoppingCartService.GetAllShoppingCartProducts<ShoppingCartViewModel>(user).ToList();
                var resultModel = new MainHeaderViewModel()
                {
                    ListCategories = allCategories,
                    WishListCount = wishProductsCount,
                    ShoppingCartProductCount = shoppingCartProducts.Count,
                    ShoppingCartTotalPrice = shoppingCartProducts.Select(x => x.Total).Sum()
                };

                return this.View(resultModel);
            }

            var resultModelIfNull = new MainHeaderViewModel()
            {
                ListCategories = allCategories,
                WishListCount = 0,
                ShoppingCartProductCount = 0,
                ShoppingCartTotalPrice = 0.00M
            };


            return View(resultModelIfNull);
        }
    }
}
