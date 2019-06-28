using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class ProductOrder
    {
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }


        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
