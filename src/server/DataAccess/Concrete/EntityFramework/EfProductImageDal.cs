using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using DataAccess.Concrete.Repository;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductImageDal : GenericRepository<ProductImage>, IProductImageDal
    {
        private readonly AppDbContext _appDbContext;

        public EfProductImageDal(AppDbContext context, AppDbContext appDbContext) : base(context)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<ProductImage>> ImagesByProduct(string urlHandle)
        {
            return await _appDbContext.ProductImages.Where(p => p.Product.UrlHandle == urlHandle).ToListAsync();
        }
    }
}
