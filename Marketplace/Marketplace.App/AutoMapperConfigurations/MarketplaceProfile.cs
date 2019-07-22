using AutoMapper;
using Marketplace.Domain;
using Marketplace.App.Areas.Administrator.ViewModels.Users;
using System.Linq;

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
        }
    }
}
