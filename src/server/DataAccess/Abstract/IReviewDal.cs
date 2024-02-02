using DataAccess.Abstract.Generic;
using Entity.Concrete;

namespace DataAccess.Abstract
{
    public interface IReviewDal : IGenericDal<Review>
    {
        Task<List<Review>> GetAllReviewsWithProductAndUser();
        Task<List<Review>> GetReviewByProduct(string urlHandle);
        Task<List<Review>> GetReviewsByUser(int userId);
        Task<Review> GetReviewByUser(int userId, string productUrlHandle);
        Task<Review> GetReviewById(int reviewId);
        Task UpdateReviewByUser(int userId, string productUrlHandle, Review review);
    }
}
