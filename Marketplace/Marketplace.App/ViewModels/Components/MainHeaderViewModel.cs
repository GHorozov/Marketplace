using Marketplace.App.ViewModels.ShoppingCart;
using System.Collections.Generic;

namespace Marketplace.App.ViewModels.Components
{
    public class MainHeaderViewModel
    {
        public List<IndexCategoryViewModel> ListCategories { get; set; }

        public int WishListCount { get; set; }

        public int ShoppingCartProductCount { get; set; }

        public decimal ShoppingCartTotalPrice { get; set; }
    }
}
