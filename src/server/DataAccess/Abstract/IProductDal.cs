using DataAccess.Abstract.Generic;
using Entity.Concrete;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IProductDal : IGenericDal<Product>
    {
        Task<List<Product>> GetAllProductsWithRelation();
        Task<List<Product>> GetAllProductsWithRelation(Expression<Func<Product, bool>> filter);

        Task<List<Product>> GetProductsByCategory(string urlHandle);

        Task<List<Product>> GetRelatedProductsByCategory(string urlHandle);

        Task<Product> GetProductWithRelation(string urlHandle);

        Task<List<Product>> SortProductsLowToHigh();
        Task<List<Product>> SortProductsLowToHigh(int? categoryId);

        Task<List<Product>> SortProductsHighToLow();
        Task<List<Product>> SortProductsHighToLow(int? categoryId);

        Task<List<Product>> PopularMobilePhones();
        Task<List<Product>> PopularLaptops();

        Task IncreaseStock(int productId, int amount);

        Task<int> CountOfProducts();

        Task<List<Product>> TopSellerProducts();

        Task<List<Product>> TopSellerProductsForHome();
    }
}
