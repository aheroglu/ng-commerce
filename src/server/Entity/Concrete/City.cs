using Entity.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<District> Districts { get; set; }
    }
}
