using Entity.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string UrlHandle { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
