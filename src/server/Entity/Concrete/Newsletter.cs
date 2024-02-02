using Entity.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Newsletter : BaseEntity
    {
        public string Email { get; set; }
    }
}
