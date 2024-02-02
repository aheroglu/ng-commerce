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
    public class CityManager : ICityService
    {
        private readonly ICityDal _cityDal;

        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }

        public async Task Delete(City entity)
        {
            await _cityDal.Delete(entity);
        }

        public async Task<List<City>> GetAll()
        {
            return await _cityDal.GetAll();
        }

        public async Task<List<City>> GetAll(Expression<Func<City, bool>> filter)
        {
            return filter == null
                ? await _cityDal.GetAll()
                : await _cityDal.GetAll(filter);
        }

        public async Task<City> GetById(int id)
        {
            return await _cityDal.GetById(id);
        }

        public async Task Insert(City entity)
        {
            await _cityDal.Insert(entity);
        }

        public async Task Update(City entity)
        {
            await _cityDal.Update(entity);
        }
    }
}
