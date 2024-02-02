using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.FavouriteDTOs
{
    public class AddFavouriteDTO
    {
        public int ProductId { get; set; }
        public int AppUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
