using Entity.Concrete;
using Entity.DTOs.CityDTOs;
using Entity.DTOs.DistrictDTOs;
using Entity.DTOs.OrderItemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.OrderDTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int OrderNo { get; set; }
        public string Address { get; set; }
        public string AddressTitle { get; set; }
        public string ZipCode { get; set; }
        public double Total { get; set; }
        public CityDTO City { get; set; }
        public DistrictDTO District { get; set; }
        public int AppUserId { get; set; }
        public string User { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
