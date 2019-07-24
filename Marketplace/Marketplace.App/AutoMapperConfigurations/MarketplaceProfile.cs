using AutoMapper;
using Marketplace.Domain;
using Marketplace.App.Areas.Administrator.ViewModels.Users;
using System.Linq;
using Marketplace.App.Areas.Administrator.ViewModels.Categories;
using Marketplace.App.ViewModels.Components;

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
            this.CreateMap<ShoppingCart, IndexShoppingCartViewModel>()
                .ForMember(isc => isc.ProductCount, sc => sc.MapFrom(x => x.Products.Count()))
                .ForMember(isc => isc.TotalPrice, sc => sc.MapFrom(x => x.Products.Sum(p => p.Product.Price)));
        }
    }
}
