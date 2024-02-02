using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.OrderDTOs
{
    public class OrderCreateDTO
    {
        public string AddressTitle { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public double Total { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int AppUserId { get; set; }
        public string Email { get; set; }
        public List<CartItemDTO> CartItems { get; set; }
    }
}
