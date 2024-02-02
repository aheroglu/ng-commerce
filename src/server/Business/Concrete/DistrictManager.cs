using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class DistrictManager : IDistrictService
    {
        private readonly IDistrictDal _districtDal;

        public DistrictManager(IDistrictDal districtDal)
        {
            _districtDal = districtDal;
        }

        public async Task Delete(District entity)
        {
            await _districtDal.Delete(entity);
        }

        public async Task<List<District>> GetAll()
        {
            return await _districtDal.GetAll();
        }

        public async Task<List<District>> GetAll(Expression<Func<District, bool>> filter)
        {
            return filter == null
                ? await _districtDal.GetAll()
                : await _districtDal.GetAll(filter);
        }

        public async Task<District> GetById(int id)
        {
            return await _districtDal.GetById(id);
        }

        public async Task<List<District>> GetDistrictsByCity(int cityId)
        {
            return await _districtDal.GetDistrictsByCity(cityId);
        }

        public async Task Insert(District entity)
        {
            await _districtDal.Insert(entity);
        }

        public async Task Update(District entity)
        {
            await _districtDal.Update(entity);
        }
    }
}
