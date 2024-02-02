using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using DataAccess.Concrete.Repository;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        private readonly AppDbContext _appDbContext;

        public EfCategoryDal(AppDbContext context, AppDbContext appDbContext) : base(context)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Category> GetByUrlHandle(string urlHandle)
        {
            return await _appDbContext.Categories.FirstOrDefaultAsync(p => p.UrlHandle == urlHandle);
        }
    }
}
