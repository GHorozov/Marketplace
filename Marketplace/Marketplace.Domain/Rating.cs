using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class Rating
    {
        public string Id { get; set; }

        public int RatingPoints { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
