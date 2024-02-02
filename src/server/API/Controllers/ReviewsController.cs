using AutoMapper;
using Business.Abstract;
using Business.ValidationRules;
using Entity.Concrete;
using Entity.DTOs.ReviewDTOs;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public ReviewsController(IReviewService reviewService, IMapper mapper, UserManager<AppUser> userManager, IProductService productService)
        {
            _reviewService = reviewService;
            _mapper = mapper;
            _userManager = userManager;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews()
        {
            var reviews = await _reviewService.GetAllReviewsWithProductAndUser();

            if (reviews.Count == 0) return NoContent();

            var result = _mapper.Map<List<ReviewDTO>>(reviews);

            return Ok(result);
        }

        [HttpGet("GetReviewById/{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var review = await _reviewService.GetReviewById(id);

            if (review is null) return NotFound();

            var result = _mapper.Map<ReviewDTO>(review);

            return Ok(result);
        }

        [HttpGet("GetReviewByUrlHandle/{urlHandle}")]
        public async Task<IActionResult> GetReviewByUrlHandle(string urlHandle)
        {
            var review = await _reviewService.GetReviewByProduct(urlHandle);

            if (review is null) return NotFound();

            var result = _mapper.Map<List<ReviewDTO>>(review);

            return Ok(result);
        }

        [HttpGet("GetReviewsByUser/{userId}")]
        public async Task<IActionResult> GetReviewsByUser(int userId)
        {
            var reviews = await _reviewService.GetReviewsByUser(userId);

            if (reviews.Count is 0) return NoContent();

            var result = _mapper.Map<List<ReviewDTO>>(reviews);

            return Ok(result);
        }

        [HttpGet("GetReviewByUser/{userId}/{productUrlHandle}")]
        public async Task<IActionResult> GetReviewByUser(int userId, string productUrlHandle)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(p => p.Id == userId);

            if (user is null) return NotFound("User Not Found!");

            var review = await _reviewService.GetReviewByUser(userId, productUrlHandle);

            if (review is null) return NotFound("Review Not Found!");

            var values = _mapper.Map<ReviewDTO>(review);

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewDTO model)
        {
            var review = _mapper.Map<Review>(model);

            ReviewValidator validator = new ReviewValidator();
            ValidationResult result = await validator.ValidateAsync(review);

            if (result.IsValid)
            {
                await _reviewService.Insert(review);
                var values = _mapper.Map<ReviewDTO>(review);
                return Ok(values);
            }

            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _reviewService.GetById(id);

            if (review is null) return NotFound();

            await _reviewService.Delete(review);

            var result = _mapper.Map<ReviewDTO>(review);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReviewByUser(UpdateReviewDTO model)
        {
            var product = await _productService.GetById(model.ProductId);
            var reviewForUpdate = await _reviewService.GetReviewByUser(model.AppUserId, product.UrlHandle);

            reviewForUpdate.Content = model.Content;
            reviewForUpdate.Rating = model.Rating;

            ReviewValidator validator = new ReviewValidator();
            ValidationResult result = await validator.ValidateAsync(reviewForUpdate);

            if (result.IsValid)
            {
                await _reviewService.UpdateReviewByUser(model.AppUserId, product.UrlHandle, reviewForUpdate);
                return Ok();
            }

            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return BadRequest(ModelState);
        }
    }
}
