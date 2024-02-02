using Business.Abstract.Generic;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductImageService : IGenericService<ProductImage>
    {
        Task<List<ProductImage>> ImagesByProduct(string urlHandle);
    }
}
