using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using DataAccess.Concrete.Repository;
using Entity.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderItemDal : GenericRepository<OrderItem>, IOrderItemDal
    {
        private readonly AppDbContext _appDbContext;

        public EfOrderItemDal(AppDbContext context, AppDbContext appDbContext) : base(context)
        {
            _appDbContext = appDbContext;
        }
    }
}
