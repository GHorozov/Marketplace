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
        public IActionResult Index()
        {
            var categories = this.categoryService.GetAllCategories<HomeCategoryViewModel>().ToList();
            var products = this.productService.GetAllProducts<HomeProductViewModel>().ToList();
            
            var resultModel = new HomeIndexViewModel()
            {
                 Categories = categories,
                 Products = products
            };

            return this.View(resultModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
