using Business.Abstract.Generic;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFavouriteService : IGenericService<Favourite>
    {
        Task<List<Favourite>> GetFavouritesByUser(int appUserId);
    }
}
