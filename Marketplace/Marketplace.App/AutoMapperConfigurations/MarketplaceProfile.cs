using AutoMapper;
using Marketplace.Domain;
using Marketplace.App.Areas.Administrator.ViewModels.Users;
using System.Linq;
using Marketplace.App.Areas.Administrator.ViewModels.Categories;
using Marketplace.App.ViewModels.Components;
using Marketplace.App.ViewModels.ShoppingCart;

namespace Marketplace.App.AutoMapperConfigurations
{
    public class MarketplaceProfile : Profile
    {
        public MarketplaceProfile()
        {
            this.CreateMap<MarketplaceUser, UserViewModel>();
            this.CreateMap<MarketplaceUser, DeleteUserViewModel>();
            this.CreateMap<MarketplaceUser, RolesViewModel>();
            this.CreateMap<MarketplaceUser, AdminChangePasswordViewModel>();

            this.CreateMap<Category, CategoryViewModel>();
            this.CreateMap<Category, EditCategoryViewModel>();

            this.CreateMap<Category, IndexCategoryViewModel>();
            this.CreateMap<ShoppingCartProduct, ShoppingCartProduct>();
        }
    }
}
