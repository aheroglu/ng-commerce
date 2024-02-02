using Entity.DTOs.ProductDTOs;

namespace Entity.DTOs.ReviewDTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public ProductDTO Product { get; set; }
        public int AppUserId { get; set; }
        public string User { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
