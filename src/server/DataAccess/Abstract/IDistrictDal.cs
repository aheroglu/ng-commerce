using DataAccess.Abstract.Generic;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IDistrictDal : IGenericDal<District>
    {
        Task<List<District>> GetDistrictsByCity(int cityId);
    }
}
