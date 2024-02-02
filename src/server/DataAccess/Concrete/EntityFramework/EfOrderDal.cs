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
    public class EfOrderDal : GenericRepository<Order>, IOrderDal
    {
        private readonly AppDbContext _appDbContext;

        public EfOrderDal(AppDbContext context, AppDbContext appDbContext) : base(context)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Order>> GetOrdersByUser(int userId)
        {
            return await _appDbContext.Orders.Include(p => p.District).Include(p => p.City).Include(p => p.AppUser).Include(p => p.OrderItems).ThenInclude(item => item.Product).Where(p => p.AppUserId == userId).OrderByDescending(p => p.CreatedAt).ToListAsync();
        }

        public async Task<Order> GetOrderDetailsByOrder(int orderNo)
        {
            return await _appDbContext.Orders.Include(p => p.District).Include(p => p.City).Include(p => p.AppUser).Include(p => p.OrderItems).ThenInclude(item => item.Product).FirstOrDefaultAsync(p => p.OrderNo == orderNo);
        }

        public async Task<List<Order>> GetOrdersWithRelations()
        {
            return await _appDbContext.Orders.Include(p => p.OrderItems).ThenInclude(p => p.Product).Include(p => p.AppUser).Include(p => p.City).Include(p => p.District).OrderByDescending(p => p.CreatedAt).ToListAsync();
        }

        public async Task<int> CountOfOrders()
        {
            return await _appDbContext.Orders.CountAsync();
        }

        public async Task<List<Order>> LastFiveOrder()
        {
            return await _appDbContext.Orders.Include(p => p.OrderItems).ThenInclude(p => p.Product).Include(p => p.AppUser).Include(p => p.City).Include(p => p.District).OrderByDescending(p => p.CreatedAt).OrderByDescending(p=>p.Id).Take(10).ToListAsync();
        }
    }
}
