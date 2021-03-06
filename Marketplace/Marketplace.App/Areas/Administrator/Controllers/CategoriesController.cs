﻿using AutoMapper;
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
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateCategoryInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var result = await this.categoryService.Create(inputModel.Name);
            if (!result)
            {
                return this.Redirect("/");
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var category = await this.categoryService.GetCategoryById(id);
            if (category == null)
            {
                return this.RedirectToAction(nameof(All));
            }
            var resultModel = this.mapper.Map<EditCategoryInputModel>(category);

            return this.View(resultModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(string id, EditCategoryInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.categoryService.Edit(id, inputModel.Name);

            return RedirectToAction(nameof(All));
        }
    }
}
