using Marketplace.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Services.Interfaces
{
    public interface IOrderService
    {
        Task<bool> Create(MarketplaceUser user, string phone, string shippingAddress);
    }
}
