using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using DataAccess.Concrete.Repository;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfReviewDal : GenericRepository<Review>, IReviewDal
    {
        private readonly AppDbContext _appDbContext;

        public EfReviewDal(AppDbContext context, AppDbContext appDbContext) : base(context)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Review>> GetAllReviewsWithProductAndUser()
        {
            return await _appDbContext.Reviews.Include(p => p.Product).Include(p => p.AppUser).ToListAsync();
        }

        public async Task<Review> GetReviewById(int reviewId)
        {
            return await _appDbContext.Reviews.Include(p => p.Product).Include(p => p.AppUser).FirstOrDefaultAsync(p => p.Id == reviewId);
        }

        public async Task<List<Review>> GetReviewByProduct(string urlHandle)
        {
            return await _appDbContext.Reviews.Include(p => p.Product).Include(p => p.AppUser).Where(p => p.Product.UrlHandle == urlHandle).ToListAsync();
        }

        public async Task<Review> GetReviewByUser(int userId, string productUrlHandle)
        {
            return await _appDbContext.Reviews.Include(p => p.AppUser).Include(p => p.Product).FirstOrDefaultAsync(p => p.AppUserId == userId && p.Product.UrlHandle == productUrlHandle);
        }

        public async Task<List<Review>> GetReviewsByUser(int userId)
        {
            return await _appDbContext.Reviews.Include(p => p.AppUser).Include(p => p.Product).Where(p => p.AppUserId == userId).ToListAsync();
        }

        public async Task UpdateReviewByUser(int userId, string productUrlHandle, Review review)
        {
            var reviewForUpdate = await _appDbContext.Reviews.FirstOrDefaultAsync(p => p.AppUserId == userId && p.Product.UrlHandle == productUrlHandle);
            reviewForUpdate.Content = review.Content;
            reviewForUpdate.Rating = review.Rating;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
