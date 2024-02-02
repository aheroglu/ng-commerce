using Entity.Concrete;
using Entity.DTOs.CategoryDTOs;
using Entity.DTOs.ProductImageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.ProductDTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string UrlHandle { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public CategoryDTO Category { get; set; }
        public int CategoryId { get; set; }
        public List<ProductImageDTO> ProductImages { get; set; }
    }
}
