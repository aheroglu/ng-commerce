using Business.Abstract.Generic;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService : IGenericService<Product>
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
