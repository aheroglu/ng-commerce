using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class ProductImageManager : IProductImageService
    {
        private readonly IProductImageDal _productImageDal;

        public ProductImageManager(IProductImageDal productImageDal)
        {
            _productImageDal = productImageDal;
        }

        public async Task Delete(ProductImage entity)
        {
            await _productImageDal.Delete(entity);
        }

        public async Task<List<ProductImage>> GetAll()
        {
            return await _productImageDal.GetAll();
        }

        public async Task<List<ProductImage>> GetAll(Expression<Func<ProductImage, bool>> filter)
        {
            return filter == null
                 ? await _productImageDal.GetAll()
                 : await _productImageDal.GetAll(filter);
        }

        public async Task<ProductImage> GetById(int id)
        {
            return await _productImageDal.GetById(id);
        }

        public async Task<List<ProductImage>> ImagesByProduct(string urlHandle)
        {
            return await _productImageDal.ImagesByProduct(urlHandle);
        }

        public async Task Insert(ProductImage entity)
        {
            await _productImageDal.Insert(entity);
        }

        public async Task Update(ProductImage entity)
        {
            await _productImageDal.Update(entity);
        }
    }
}
