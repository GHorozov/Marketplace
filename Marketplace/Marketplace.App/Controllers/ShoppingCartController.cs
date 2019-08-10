using AutoMapper;
using Marketplace.App.Helpers;
using Marketplace.App.Infrastructure;
using Marketplace.App.ViewModels.ShoppingCart;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly UserManager<MarketplaceUser> userManager;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IProductService productService;

        public ShoppingCartController(UserManager<MarketplaceUser> userManager, IShoppingCartService shoppingCartService, IProductService productService)
        {
            this.userManager = userManager;
            this.shoppingCartService = shoppingCartService;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.GetUserAsync(HttpContext.User);
                var allProductsInCart = await this.shoppingCartService
                .GetAllShoppingCartProducts<ShoppingCartViewModel>(user).ToListAsync();

                var resultModel = new ShoppingCartProductsViewModel()
                {
                    Products = allProductsInCart
                };

                return this.View(resultModel);
            }

            var cartProductsFromSession = this.GetSessionShoppingCart();
            var resultModelFromSeesion = new ShoppingCartProductsViewModel()
            {
                Products = cartProductsFromSession.ToList()
            };

            return this.View(resultModelFromSeesion);
        }

        [HttpPost]
        public async Task<IActionResult> Add(string id, int quantity)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.GetUserAsync(HttpContext.User);
                var result = await this.shoppingCartService.AddProductToShoppingCartAsync(id, user.UserName, quantity);
                if (!result)
                {
                    this.Redirect("/");
                }
            }
            else
            {
                var cartProductsFromSession = this.GetSessionShoppingCart();
                var cart = cartProductsFromSession.ToList();

                var product = await this.productService.GetProductById(id);
                var productToAdd = new ShoppingCartViewModel()
                {
                    Id = product.Id,
                    Color = product.Color,
                    Name = product.Name,
                    PictureUrl = product.Pictures.First().PictureUrl,
                    Price = product.Price,
                    Quantity = quantity
                };
                cart.Add(productToAdd);
                this.HttpContext.Session.SetObjectToJson(GlobalConstants.ShoppingCartKey, cart);
            }

            return this.Redirect("/");
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.GetUserAsync(HttpContext.User);
                var result = await this.shoppingCartService.Delete(id, user.Id);
                if (!result)
                {
                    return this.Redirect("/");
                }
            }
            else
            {
                var cartProductsFromSession = GetSessionShoppingCart();
                var cart = cartProductsFromSession.ToList();
                var product =await this.productService.GetProductById(id);
                var productToRemove = cart.Single(x => x.Id == product.Id);
                cart.Remove(productToRemove);
                this.HttpContext.Session.SetObjectToJson(GlobalConstants.ShoppingCartKey, cart);
            }

            return this.RedirectToAction(nameof(Cart));
        }

        public async Task<IActionResult> ClearCart()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.GetUserAsync(HttpContext.User);
                var result = await this.shoppingCartService.ClearCart(user);
                if (!result)
                {
                    return this.Redirect("/");
                }
            }
            else
            {
                var cartProductsFromSession = GetSessionShoppingCart();
                var cart = cartProductsFromSession.ToList();
                cart.Clear();
                this.HttpContext.Session.SetObjectToJson(GlobalConstants.ShoppingCartKey, cart);
            }

            return this.RedirectToAction(nameof(Cart));
        }

        private ShoppingCartViewModel[] GetSessionShoppingCart()
        {
            return this.HttpContext
                .Session
                .GetObjectFromJson<ShoppingCartViewModel[]>(GlobalConstants.ShoppingCartKey) ?? new List<ShoppingCartViewModel>().ToArray();
        }
    }
}
