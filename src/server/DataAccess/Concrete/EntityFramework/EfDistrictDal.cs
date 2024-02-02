using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using DataAccess.Concrete.Repository;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfDistrictDal : GenericRepository<District>, IDistrictDal
    {
        private readonly AppDbContext _appDbContext;

        public EfDistrictDal(AppDbContext context, AppDbContext appDbContext) : base(context)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<District>> GetDistrictsByCity(int cityId)
        {
            return await _appDbContext.Districts.Include(p => p.City).Where(p => p.CityId == cityId).ToListAsync();
        }
    }
}
