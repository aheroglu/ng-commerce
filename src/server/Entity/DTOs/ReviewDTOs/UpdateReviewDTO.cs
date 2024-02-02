using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.ReviewDTOs
{
    public class UpdateReviewDTO
    {
        public string Content { get; set; }
        public int Rating { get; set; }
        public int ProductId { get; set; }
        public int AppUserId { get; set; }
    }
}
