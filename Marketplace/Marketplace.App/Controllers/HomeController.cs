using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Marketplace.App.Models;
using Microsoft.AspNetCore.Authorization;
using Marketplace.Services.Interfaces;
using Marketplace.App.ViewModels.Home;
using AutoMapper;
using System.Threading.Tasks;
using Marketplace.App.Infrastructure;

namespace Marketplace.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public HomeController(IMapper mapper, ICategoryService categoryService, IProductService productService)
        {
            this.mapper = mapper;
            this.categoryService = categoryService;
            this.productService = productService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = this.productService.GetAllProducts<HomeProductViewModel>().ToList();

            var resultModel = new HomeIndexViewModel()
            {
                Products = products
            };

            return this.View(resultModel);
        }

        [HttpGet]
        public IActionResult Search(HomeSearchInputModel inputModel)
        {
            this.ViewData["ProductsHead"] = GlobalConstants.HeadTextForFoundResult;
            if (inputModel.Input == string.Empty && inputModel.CategoryName == GlobalConstants.SearchCategoryDefaultValue)
            {
                return this.Redirect(nameof(Index));
            }
            else if(inputModel.Input != string.Empty && inputModel.CategoryName != GlobalConstants.SearchCategoryDefaultValue)
            {
                var resultModel = this.productService.GetProductByInputAndCategoryName<HomeSearchViewModel>(inputModel.Input, inputModel.CategoryName).ToList();

                return this.View(resultModel);
            }
            else if (inputModel.Input != string.Empty)
            {
                var resultModel = this.productService.GetProductByInput<HomeSearchViewModel>(inputModel.Input, inputModel.CategoryName).ToList();

                return this.View(resultModel);
            }
            else if (inputModel.CategoryName != GlobalConstants.SearchCategoryDefaultValue)
            {
                var resultModel = this.productService.GetProductByCategoryName<HomeSearchViewModel>(inputModel.Input, inputModel.CategoryName).ToList();

                return this.View(resultModel);
            }

            return this.View(new List<HomeSearchViewModel>());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
