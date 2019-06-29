using Marketplace.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Data
{
    public class MarketplaceDbContext : IdentityDbContext<MarketplaceUser>
    {


        public MarketplaceDbContext(DbContextOptions options) 
            : base(options)
        {
        }
    }
}
