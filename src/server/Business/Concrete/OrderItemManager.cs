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
    public class OrderItemManager : IOrderItemService
    {
        private readonly IOrderItemDal _orderItemDal;

        public OrderItemManager(IOrderItemDal orderItemDal)
        {
            _orderItemDal = orderItemDal;
        }

        public async Task Delete(OrderItem entity)
        {
            await _orderItemDal.Delete(entity);
        }

        public async Task<List<OrderItem>> GetAll()
        {
            return await _orderItemDal.GetAll();
        }

        public async Task<List<OrderItem>> GetAll(Expression<Func<OrderItem, bool>> filter)
        {
            return filter == null
                ? await _orderItemDal.GetAll()
                : await _orderItemDal.GetAll(filter);
        }

        public async Task<OrderItem> GetById(int id)
        {
            return await _orderItemDal.GetById(id);
        }

        public async Task Insert(OrderItem entity)
        {
            await _orderItemDal.Insert(entity);
        }

        public async Task Update(OrderItem entity)
        {
            await _orderItemDal.Update(entity);
        }
    }
}
