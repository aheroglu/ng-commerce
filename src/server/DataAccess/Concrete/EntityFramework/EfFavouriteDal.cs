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
    public class EfFavouriteDal : GenericRepository<Favourite>, IFavouriteDal
    {
        private readonly AppDbContext _appDbContext;

        public EfFavouriteDal(AppDbContext context, AppDbContext appDbContext) : base(context)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Favourite>> GetFavouritesByUser(int appUserId)
        {
            return await _appDbContext.Favourites.Include(p => p.Product).Include(p => p.AppUser).Where(p => p.AppUserId == appUserId).ToListAsync();
        }
    }
}
