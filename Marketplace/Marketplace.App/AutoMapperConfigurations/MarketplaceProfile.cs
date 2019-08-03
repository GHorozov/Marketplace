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
            //MarketplaceUser
            this.CreateMap<MarketplaceUser, UserViewModel>();
            this.CreateMap<MarketplaceUser, DeleteUserViewModel>();
            this.CreateMap<MarketplaceUser, RolesViewModel>();
            this.CreateMap<MarketplaceUser, AdminChangePasswordInputModel>();

            //Category
            this.CreateMap<Category, CategoryViewModel>();
            this.CreateMap<Category, EditCategoryInputModel>();
            this.CreateMap<Category, ProductCategoryViewModel>();
            this.CreateMap<Category, HomeCategoryViewModel>();
            this.CreateMap<Category, IndexCategoryViewModel>();

            //Product
            this.CreateMap<Product, HomeProductViewModel>()
                .ForMember(hpvm => hpvm.PictureUrl, x => x.MapFrom(p => p.Pictures.First().PictureUrl));

            this.CreateMap<Product, AllProductViewModel>()
                .ForMember(apvm => apvm.Picture, x => x.MapFrom(p => p.Pictures.First().PictureUrl));

            this.CreateMap<Product, DetailsProductViewModel>()
                .ForMember(dpvm => dpvm.CategoryName, x => x.MapFrom(p => p.Category.Name))
                .ForMember(dpvm => dpvm.ProductName, x => x.MapFrom(p => p.Name));
                
            this.CreateMap<Product, MyProductViewModel>()
                .ForMember(mpvm => mpvm.Picture, x => x.MapFrom(p => p.Pictures.First().PictureUrl));

            this.CreateMap<Product, EditProductInputModel>();
            this.CreateMap<EditProductInputModel, Product>();
                

            //ShoppingCart
            this.CreateMap<Product, ShoppingCartViewModel>();
            this.CreateMap<ShoppingCartProduct, ShoppingCartViewModel>()
                .ForMember(scvm => scvm.Id, x => x.MapFrom(p => p.Product.Id))
                .ForMember(scvm => scvm.PictureUrl, x => x.MapFrom(p => p.Product.Pictures.First().PictureUrl))
                .ForMember(scvm => scvm.Name, x => x.MapFrom(p => p.Product.Name))
                .ForMember(scvm => scvm.Color, x => x.MapFrom(p => p.Product.Color))
                .ForMember(scvm => scvm.Price, x => x.MapFrom(p => p.Product.Price));
                
        }
    }
}
