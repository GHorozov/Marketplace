using AutoMapper;
using Marketplace.App.Areas.Administrator.ViewModels.Categories;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.Areas.Administrator.Controllers
{
    public class CategoriesController : AdministratorController
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult All()
        {
            var resultModel = this.categoryService.GetAllCategories<CategoryViewModel>();

            return View(resultModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            this.categoryService.Create(inputModel.Name);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var category = this.categoryService.GetCategoryById(id);
            if(category == null)
            {
                return NotFound();
            }
            var resultModel = this.mapper.Map<EditCategoryViewModel>(category);

            return this.View(resultModel);
        }

        [HttpPost]
        public IActionResult Edit(string id, EditCategoryViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            this.categoryService.Edit(id, inputModel.Name);

            return RedirectToAction(nameof(All));
        }
    }
}
