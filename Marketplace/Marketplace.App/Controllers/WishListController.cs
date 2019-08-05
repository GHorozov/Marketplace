using AutoMapper;
using Marketplace.App.ViewModels.WishList;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.Controllers
{
    [Authorize]
    public class WishListController : Controller
    {
        private readonly IMapper mapper;
        private readonly UserManager<MarketplaceUser> userManager;
        private readonly IWishProductService wishProductService;

        public WishListController(IMapper mapper, UserManager<MarketplaceUser> userManager, IWishProductService wishProductService)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.wishProductService = wishProductService;
        }
        
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            var resultModel = this.wishProductService.GetAllWishProducts<WishListProductViewModel>(user).ToList();

            return this.View(resultModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add(string id)
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            var result = await this.wishProductService.Add(user, id);
            if (!result)
            {
                //to do:
                return this.Redirect("/");
            }

            return this.RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            var result = await this.wishProductService.Detele(user, id);
            if (!result)
            {
                //to do:
                return this.Redirect("/");
            }

            return this.RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> ClearAll()
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            var result = await this.wishProductService.ClearAll(user);
            if (!result)
            {
                //to do:
                return this.Redirect("/");
            }

            return this.RedirectToAction(nameof(All));
        }
    }
}
