using Marketplace.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Services.Interfaces
{
    public interface IWishProductService
    {
        int GetAllProductsCount(MarketplaceUser user);
    }
}
