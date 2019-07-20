using AutoMapper;
using Marketplace.Domain;
using Marketplace.App.Areas.Administrator.ViewModels.Users;

namespace Marketplace.App.AutoMapperConfigurations
{
    public class MarketplaceProfile : Profile
    {
        public MarketplaceProfile()
        {
            this.CreateMap<MarketplaceUser, UserViewModel>();
        }
    }
}
