using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.Infrastructure
{
    public class GlobalConstants
    {
        public const string AdministratorRole = "Administrator";
        public const string UserRole = "User";

        public const string ShoppingCartKey = "%ShoppingCartKey%";


        public const string PriceMinValue = "0.00";
        public const string PriceMaxValue = "79228162514264337593543950335";

        public const string DefaultPicturesPath = @"C:\GitHub\Marketplace\Marketplace\Marketplace.App\wwwroot\images\";
    }
}
