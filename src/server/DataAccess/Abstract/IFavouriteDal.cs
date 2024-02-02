using DataAccess.Abstract.Generic;
using Entity.Concrete;

namespace DataAccess.Abstract
{
    public interface IFavouriteDal : IGenericDal<Favourite>
    {
        Task<List<Favourite>> GetFavouritesByUser(int appUserId);
    }
}
