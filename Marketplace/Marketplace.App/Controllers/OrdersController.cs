using Marketplace.App.ViewModels.Orders;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.Controllers
{
    
    public class OrdersController : Controller
    {
        private readonly UserManager<MarketplaceUser> userManager;
        private readonly IOrderService orderService;
        private readonly IShoppingCartService shoppingCartService;

        public OrdersController(UserManager<MarketplaceUser> userManager, IOrderService orderService, IShoppingCartService shoppingCartService)
        {
            this.userManager = userManager;
            this.orderService = orderService;
            this.shoppingCartService = shoppingCartService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            var isCartAny = await this.shoppingCartService.IsCartAny(user);
            if (!isCartAny)
            {
                return this.Redirect("/ShoppingCart/Cart");
            }

            return this.View();
        } 

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(HttpContext.User);
            var result = await this.orderService.Create(user, inputModel.Phone, inputModel.ShippingAddress);
            if (!result)
            {
                //to do
                return this.Redirect("/");
            }

            return this.RedirectToAction(nameof(SuccessfulOrder));
        }

        [Authorize]
        [HttpGet]
        public IActionResult SuccessfulOrder()
        {
            return this.View();
        }
    }
}
