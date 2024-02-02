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
    public class EfNewsletterDal : GenericRepository<Newsletter>, INewsletterDal
    {
        private readonly AppDbContext _appDbContext;

        public EfNewsletterDal(AppDbContext context, AppDbContext appDbContext) : base(context)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> CountOfSubscribers()
        {
            return await _appDbContext.Newsletters.CountAsync();
        }

        public async Task<Newsletter> GetNewsletterByEmail(string email)
        {
            return await _appDbContext.Newsletters.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<bool> IsSubscribed(string email)
        {
            return await _appDbContext.Newsletters.AnyAsync(x => x.Email == email);
        }
    }
}
