using AutoMapper;
using Marketplace.Domain;
using Marketplace.App.Areas.Administrator.ViewModels.Users;
using System.Linq;
using Marketplace.App.Areas.Administrator.ViewModels.Categories;
using Marketplace.App.ViewModels.Components;
using Marketplace.App.ViewModels.ShoppingCart;
using Marketplace.App.ViewModels.Products;
using Marketplace.App.ViewModels.Home;

namespace Marketplace.App.AutoMapperConfigurations
{
    public class MarketplaceProfile : Profile
    {
        public MarketplaceProfile()
        {
            this.CreateMap<MarketplaceUser, UserViewModel>();
            this.CreateMap<MarketplaceUser, DeleteUserViewModel>();
            this.CreateMap<MarketplaceUser, RolesViewModel>();
            this.CreateMap<MarketplaceUser, AdminChangePasswordInputModel>();

            this.CreateMap<Category, CategoryViewModel>();
            this.CreateMap<Category, EditCategoryInputModel>();
            this.CreateMap<Category, ProductCategoryViewModel>();
            this.CreateMap<Category, HomeCategoryViewModel>();

            this.CreateMap<Category, IndexCategoryViewModel>();

            this.CreateMap<ShoppingCartProduct, ShoppingCartProduct>();

            this.CreateMap<Product, DetailsProductViewModel>()
                .ForMember(dpvm => dpvm.CategoryName, x => x.MapFrom(p => p.Category.Name))
                .ForMember(dpvm => dpvm.ProductName, x => x.MapFrom(p => p.Name));
                

            this.CreateMap<Product, MyProductViewModel>();
            this.CreateMap<Product, HomeProductViewModel>()
                .ForMember(hpvm => hpvm.PictureUrl, x => x.MapFrom(p => p.Pictures.First().PictureUrl));

            this.CreateMap<Product, AllProductViewModel>()
                .ForMember(apvm => apvm.Picture, x => x.MapFrom(p => p.Pictures.First().PictureUrl));
        }
    }
}
