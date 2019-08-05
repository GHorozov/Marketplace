using AutoMapper;
using Marketplace.App.Infrastructure;
using Marketplace.App.ViewModels.Products;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IMapper mapper;
        private readonly UserManager<MarketplaceUser> userManager;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly IPictureService pictureService;

        public ProductsController(IMapper mapper, UserManager<MarketplaceUser> userManager, ICategoryService categoryService, IProductService productService, IPictureService pictureService)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.categoryService = categoryService;
            this.productService = productService;
            this.pictureService = pictureService;
        }

        public IActionResult All()
        {
            var resultModel = this.productService.GetAllProducts<AllProductViewModel>().ToList();

            return this.View(resultModel);
        }

        [Authorize]
        public async Task<IActionResult> My()
        {
            var user = await this.userManager.FindByEmailAsync(User.Identity.Name);

            var resultModel = this.productService.GetMyProducts<MyProductViewModel>(user.Id).ToList();

            return this.View(resultModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            CreateProductInputModel inputModel = PrepareCreateProductInputModel();

            return this.View(inputModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                inputModel = PrepareCreateProductInputModel();

                return this.View(inputModel);
            }

            var user = await this.userManager.FindByEmailAsync(User.Identity.Name);

            var product = new Product()
            {
                Name = inputModel.Name,
                Quantity = inputModel.Quantity,
                Price = inputModel.Price,
                Description = inputModel.Description,
                PublishDate = DateTime.UtcNow,
                Color = inputModel.Color,
                MarketplaceUser = user,
            };

            var isProductAdded = await this.productService.AddProduct(product);
            if (!isProductAdded)
            {
                inputModel = PrepareCreateProductInputModel();

                return this.View(inputModel);
            }

            var category = this.categoryService.GetCategoryByName(inputModel.CategoryName);
            await this.productService.AddCategory(product.Id, category);

            var picturePath = await this.pictureService
                .SavePicture(product.Id, inputModel.Picture, GlobalConstants.DefaultPicturesPath);

            await this.productService.AddPicturePath(product.Id, picturePath);

            return this.RedirectToAction(nameof(My));
        }

        public async Task<IActionResult> Details(string id)
        {
            var product = await this.productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            
            var resultModel = this.mapper.Map<DetailsProductViewModel>(product);
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            if(user == null)
            {
                return this.View(resultModel);
            }
            resultModel.IsMyProduct = product.MarketplaceUserId == user.Id ? true : false;

            return this.View(resultModel);
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var product = productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            var allCategories = this.categoryService.GetAllCategories<ProductCategoryViewModel>().ToList();
            var categories = allCategories.Select(x => new SelectListItem() { Text = x.Name }).ToList();
            var inputModel = this.mapper.Map<EditProductInputModel>(product);
            inputModel.Categories = categories;

            return this.View(inputModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditProductInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                var allCategories = this.categoryService.GetAllCategories<ProductCategoryViewModel>().ToList();
                var categories = allCategories.Select(x => new SelectListItem() { Text = x.Name }).ToList();
                inputModel.Categories = categories;

                return this.View(inputModel);
            }

            var product = this.mapper.Map<Product>(inputModel);
            var category = this.categoryService.GetCategoryByName(inputModel.CategoryName);
            product.Category = category;

            var editedProduct = await this.productService.EditProduct(product);

            var picturePath = await this.pictureService
                .SavePicture(editedProduct.Id, inputModel.Picture, GlobalConstants.DefaultPicturesPath);

            await this.productService.EditPicturePath(editedProduct.Id, picturePath);

            return this.RedirectToAction(nameof(My));
        }

        [Authorize]
        public async Task<IActionResult> AddPicture(string id)
        {
            var product = await this.productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            var inputModel = new AddPictureProductInputModel() { Id = product.Id };

            return this.View(inputModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPicture(AddPictureProductInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var picturePath = await this.pictureService
               .SavePicture(inputModel.Id, inputModel.Picture, GlobalConstants.DefaultPicturesPath);

            await this.productService.AddPicturePath(inputModel.Id, picturePath);

            return this.Redirect($"/Products/Details/{inputModel.Id}");
        }


        private CreateProductInputModel PrepareCreateProductInputModel()
        {
            var allCategories = this.categoryService.GetAllCategories<ProductCategoryViewModel>().ToList();
            var categories = allCategories.Select(x => new SelectListItem() { Text = x.Name }).ToList();

            var inputModel = new CreateProductInputModel() { Categories = categories };
            return inputModel;
        }
    }
}
