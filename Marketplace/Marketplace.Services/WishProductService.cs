using Marketplace.Data;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Services
{
    public class WishProductService : IWishProductService
    {
        private readonly MarketplaceDbContext context;

        public WishProductService(MarketplaceDbContext context)
        {
            this.context = context;
        }

        public int GetAllProductsCount(MarketplaceUser user)
        {
            if (user != null)
            {
                var productCount = user.WishProducts.Count;

                return productCount;
            }

            return 0;
        }
    }
}
