using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FavouriteManager : IFavouriteService
    {
        private readonly IFavouriteDal _favouriteDal;

        public FavouriteManager(IFavouriteDal favouriteDal)
        {
            _favouriteDal = favouriteDal;
        }

        public async Task Delete(Favourite entity)
        {
            await _favouriteDal.Delete(entity);
        }

        public async Task<List<Favourite>> GetAll()
        {
            return await _favouriteDal.GetAll();
        }

        public async Task<List<Favourite>> GetAll(Expression<Func<Favourite, bool>> filter)
        {
            return filter == null
                ? await _favouriteDal.GetAll()
                : await _favouriteDal.GetAll(filter);
        }

        public async Task<Favourite> GetById(int id)
        {
            return await _favouriteDal.GetById(id);
        }

        public async Task<List<Favourite>> GetFavouritesByUser(int appUserId)
        {
            return await _favouriteDal.GetFavouritesByUser(appUserId);
        }

        public async Task Insert(Favourite entity)
        {
            await _favouriteDal.Insert(entity);
        }

        public async Task Update(Favourite entity)
        {
            await _favouriteDal.Update(entity);
        }
    }
}
