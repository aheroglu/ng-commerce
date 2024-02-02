using Entity.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string UrlHandle { get; set; }
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
