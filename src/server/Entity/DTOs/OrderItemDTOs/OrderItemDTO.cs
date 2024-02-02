using Entity.DTOs.OrderDTOs;
using Entity.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.OrderItemDTOs
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public OrderDTO Order { get; set; }
        public ProductDTO Product { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
