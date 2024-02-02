using Entity.Concrete.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Order : BaseEntity
    {
        public int OrderNo { get; set; }
        public string Address { get; set; }
        public string AddressTitle { get; set; }
        public string ZipCode { get; set; }
        public double Total { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
