using DataAccess.Abstract.Generic;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductImageDal : IGenericDal<ProductImage>
    {
        Task<List<ProductImage>> ImagesByProduct(string urlHandle);
    }
}
