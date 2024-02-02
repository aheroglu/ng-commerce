using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ReviewManager : IReviewService
    {
        private readonly IReviewDal _reviewDal;

        public ReviewManager(IReviewDal reviewDal)
        {
            _reviewDal = reviewDal;
        }

        public async Task Delete(Review entity)
        {
            await _reviewDal.Delete(entity);
        }

        public async Task<List<Review>> GetAll()
        {
            return await _reviewDal.GetAll();
        }

        public async Task<List<Review>> GetAll(Expression<Func<Review, bool>> filter)
        {
            return filter == null
                ? await _reviewDal.GetAll()
                : await _reviewDal.GetAll(filter);
        }

        public async Task<Review> GetById(int id)
        {
            return await _reviewDal.GetById(id);
        }

        public async Task<List<Review>> GetAllReviewsWithProductAndUser()
        {
            return await _reviewDal.GetAllReviewsWithProductAndUser();
        }

        public async Task<List<Review>> GetReviewByProduct(string urlHandle)
        {
            return await _reviewDal.GetReviewByProduct(urlHandle);
        }

        public async Task Insert(Review entity)
        {
            await _reviewDal.Insert(entity);
        }

        public async Task Update(Review entity)
        {
            await _reviewDal.Update(entity);
        }

        public async Task<Review> GetReviewByUser(int userId, string productUrlHandle)
        {
            return await _reviewDal.GetReviewByUser(userId, productUrlHandle);
        }

        public async Task UpdateReviewByUser(int userId, string productUrlHandle, Review review)
        {
            await _reviewDal.UpdateReviewByUser(userId, productUrlHandle, review);
        }

        public async Task<List<Review>> GetReviewsByUser(int userId)
        {
            return await _reviewDal.GetReviewsByUser(userId);
        }

        public async Task<Review> GetReviewById(int reviewId)
        {
            return await _reviewDal.GetReviewById(reviewId);
        }
    }
}
