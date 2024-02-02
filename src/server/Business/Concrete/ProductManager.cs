using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task Delete(Product entity)
        {
            await _productDal.Delete(entity);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _productDal.GetAll();
        }

        public async Task<List<Product>> GetAll(Expression<Func<Product, bool>> filter)
        {
            return filter == null
                ? await _productDal.GetAll()
                : await _productDal.GetAll(filter);
        }

        public async Task<Product> GetById(int id)
        {
            return await _productDal.GetById(id);
        }

        public async Task<List<Product>> GetAllProductsWithRelation()
        {
            return await _productDal.GetAllProductsWithRelation();
        }

        public async Task<List<Product>> GetAllProductsWithRelation(Expression<Func<Product, bool>> filter)
        {
            return filter == null
                ? await _productDal.GetAllProductsWithRelation()
                : await _productDal.GetAllProductsWithRelation(filter);
        }

        public async Task<Product> GetProductWithRelation(string urlHandle)
        {
            return await _productDal.GetProductWithRelation(urlHandle);
        }

        public async Task Insert(Product entity)
        {
            await _productDal.Insert(entity);
        }

        public async Task Update(Product entity)
        {
            await _productDal.Update(entity);
        }

        public async Task<List<Product>> GetProductsByCategory(string urlHandle)
        {
            return await _productDal.GetProductsByCategory(urlHandle);
        }

        public async Task<List<Product>> GetRelatedProductsByCategory(string urlHandle)
        {
            return await _productDal.GetRelatedProductsByCategory(urlHandle);
        }

        public async Task<List<Product>> SortProductsLowToHigh()
        {
            return await _productDal.SortProductsLowToHigh();
        }

        public async Task<List<Product>> SortProductsLowToHigh(int? categoryId)
        {
            return categoryId == null
                ? await _productDal.SortProductsLowToHigh(categoryId)
                : await _productDal.SortProductsLowToHigh(categoryId);
        }

        public async Task<List<Product>> SortProductsHighToLow()
        {
            return await _productDal.SortProductsHighToLow();
        }

        public async Task<List<Product>> SortProductsHighToLow(int? categoryId)
        {
            return categoryId == null
                ? await _productDal.SortProductsHighToLow(categoryId)
                : await _productDal.SortProductsHighToLow(categoryId);
        }

        public async Task<List<Product>> PopularMobilePhones()
        {
            return await _productDal.PopularMobilePhones();
        }

        public async Task<List<Product>> PopularLaptops()
        {
            return await _productDal.PopularLaptops();
        }

        public async Task IncreaseStock(int productId, int amount)
        {
            await _productDal.IncreaseStock(productId, amount);
        }

        public async Task<int> CountOfProducts()
        {
            return await _productDal.CountOfProducts();
        }

        public async Task<List<Product>> TopSellerProducts()
        {
            return await _productDal.TopSellerProducts();
        }

        public async Task<List<Product>> TopSellerProductsForHome()
        {
            return await _productDal.TopSellerProductsForHome();
        }
    }
}
