using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using DataAccess.Concrete.Repository;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : GenericRepository<Product>, IProductDal
    {
        private readonly AppDbContext _appDbContext;

        public EfProductDal(AppDbContext context, AppDbContext appDbContext) : base(context)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Product>> GetAllProductsWithRelation()
        {
            var products = await _appDbContext.Products.Include(p => p.Category).Include(p => p.ProductImages).ToListAsync();
            var random = new Random();
            var shuffledProducts = products.OrderBy(x => random.Next()).ToList();
            return shuffledProducts;
        }

        public async Task<List<Product>> GetAllProductsWithRelation(Expression<Func<Product, bool>> filter)
        {
            return filter == null
                ? await _appDbContext.Products.Include(p => p.Category).Include(p => p.ProductImages).ToListAsync()
                : await _appDbContext.Products.Include(p => p.Category).Include(p => p.ProductImages).Where(filter).ToListAsync();
        }

        public async Task<List<Product>> GetProductsByCategory(string urlHandle)
        {
            return await _appDbContext.Products.Include(p => p.Category).Include(p => p.ProductImages).Where(p => p.Category.UrlHandle == urlHandle).ToListAsync();
        }

        public async Task<List<Product>> GetRelatedProductsByCategory(string urlHandle)
        {
            var products = await _appDbContext.Products.Include(p => p.Category).Include(p => p.ProductImages).Where(p => p.Category.UrlHandle == urlHandle).ToListAsync();
            var random = new Random();
            var shuffledProducts = products.OrderBy(x => random.Next()).Take(4).ToList();
            return shuffledProducts;
        }

        public async Task<Product> GetProductWithRelation(string urlHandle)
        {
            return await _appDbContext.Products.Include(p => p.Category).Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.UrlHandle == urlHandle);
        }

        public async Task<List<Product>> SortProductsLowToHigh()
        {
            return await _appDbContext.Products.Include(p => p.Category).Include(p => p.ProductImages).OrderBy(p => p.Price).ToListAsync();
        }

        public async Task<List<Product>> SortProductsLowToHigh(int? categoryId)
        {
            return categoryId == null
                ? await _appDbContext.Products.Include(p => p.Category).Include(p => p.ProductImages).OrderBy(p => p.Price).ToListAsync()
                : await _appDbContext.Products.Include(p => p.Category).Include(p => p.ProductImages).Where(p => p.CategoryId == categoryId).OrderBy(p => p.Price).ToListAsync();
        }

        public async Task<List<Product>> SortProductsHighToLow()
        {
            return await _appDbContext.Products.Include(p => p.Category).Include(p => p.ProductImages).OrderByDescending(p => p.Price).ToListAsync();
        }

        public async Task<List<Product>> SortProductsHighToLow(int? categoryId)
        {
            return categoryId == null
                ? await _appDbContext.Products.Include(p => p.Category).Include(p => p.ProductImages).OrderByDescending(p => p.Price).ToListAsync()
                : await _appDbContext.Products.Include(p => p.Category).Include(p => p.ProductImages).Where(p => p.CategoryId == categoryId).OrderByDescending(p => p.Price).ToListAsync();
        }

        public async Task<List<Product>> PopularMobilePhones()
        {
            var productIds = await _appDbContext.OrderItems
                .Where(p => p.Product.Category.Name == "Mobile Phones")
                .GroupBy(p => p.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    TotalQuantity = group.Sum(p => p.Quantity)
                })
                .OrderByDescending(result => result.TotalQuantity)
                .ThenBy(result => result.ProductId)
                .Select(result => result.ProductId)
                .Take(4)
                .ToListAsync();

            var products = await _appDbContext.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            products = products.OrderBy(p => productIds.IndexOf(p.Id)).ToList();

            if (products.Count == 0)
            {
                return await _appDbContext.Products.Where(p => p.Category.Name == "Mobile Phones").Take(4).ToListAsync();
            }

            return products;
        }

        public async Task<List<Product>> PopularLaptops()
        {
            var productIds = await _appDbContext.OrderItems
                .Where(p => p.Product.Category.Name == "Laptops")
                .GroupBy(p => p.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    TotalQuantity = group.Sum(p => p.Quantity)
                })
                .OrderByDescending(result => result.TotalQuantity)
                .ThenBy(result => result.ProductId)
                .Select(result => result.ProductId)
                .Take(4)
                .ToListAsync();

            var products = await _appDbContext.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            products = products.OrderBy(p => productIds.IndexOf(p.Id)).ToList();

            if (products.Count == 0)
            {
                return await _appDbContext.Products.Where(p => p.Category.Name == "Laptops").Take(4).ToListAsync();
            }

            return products;
        }

        public async Task IncreaseStock(int productId, int amount)
        {
            var product = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            product.StockQuantity = product.StockQuantity - amount;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<int> CountOfProducts()
        {
            return await _appDbContext.Products.CountAsync();
        }

        public async Task<List<Product>> TopSellerProducts()
        {
            var productIds = await _appDbContext.OrderItems
                .GroupBy(p => p.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    TotalQuantity = group.Sum(p => p.Quantity)
                })
                .OrderByDescending(result => result.TotalQuantity)
                .ThenBy(result => result.ProductId)
                .Select(result => result.ProductId)
                .Take(10)
                .ToListAsync();

            var products = await _appDbContext.Products
                .Include(p => p.Category)
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            return products;
        }

        public async Task<List<Product>> TopSellerProductsForHome()
        {
            var productIds = await _appDbContext.OrderItems
                .GroupBy(p => p.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    TotalQuantity = group.Sum(p => p.Quantity)
                })
                .OrderByDescending(result => result.TotalQuantity)
                .ThenBy(result => result.ProductId)
                .Select(result => result.ProductId)
                .Take(4)
                .ToListAsync();

            var products = await _appDbContext.Products
                .Include(p => p.Category)
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            return products;

        }
    }
}
