using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class Picture
    {
        public string Id { get; set; }

        public string PictureUrl { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
