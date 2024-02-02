using Entity.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.FavouriteDTOs
{
    public class FavouriteDTO
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string User { get; set; }
        public ProductDTO Product { get; set; }
    }
}
