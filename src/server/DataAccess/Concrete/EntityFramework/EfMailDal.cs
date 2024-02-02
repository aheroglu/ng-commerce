using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using DataAccess.Concrete.Repository;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMailDal : GenericRepository<Mail>, IMailDal
    {
        private readonly AppDbContext _appDbContext;

        public EfMailDal(AppDbContext context, AppDbContext appDbContext) : base(context)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Mail>> MailsByMembership(string membership)
        {
            return await _appDbContext.Mail.Where(p => p.For == membership).ToListAsync();
        }
    }
}
