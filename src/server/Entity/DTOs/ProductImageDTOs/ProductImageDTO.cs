using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.ProductImageDTOs
{
    public class ProductImageDTO
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ProductId { get; set; }
    }
}
