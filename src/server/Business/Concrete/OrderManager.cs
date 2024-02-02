using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public Task<int> CountOfOrders()
        {
            return _orderDal.CountOfOrders();
        }

        public async Task Delete(Order entity)
        {
            await _orderDal.Delete(entity);
        }

        public async Task<List<Order>> GetAll()
        {
            return await _orderDal.GetAll();
        }

        public async Task<List<Order>> GetAll(Expression<Func<Order, bool>> filter)
        {
            return filter == null
                ? await _orderDal.GetAll()
                : await _orderDal.GetAll(filter);
        }

        public Task<Order> GetById(int id)
        {
            return _orderDal.GetById(id);
        }

        public async Task<Order> GetOrderDetailsByOrder(int orderNo)
        {
            return await _orderDal.GetOrderDetailsByOrder(orderNo);
        }

        public async Task<List<Order>> GetOrdersByUser(int userId)
        {
            return await _orderDal.GetOrdersByUser(userId);
        }

        public async Task<List<Order>> GetOrdersWithRelations()
        {
            return await _orderDal.GetOrdersWithRelations();
        }

        public async Task Insert(Order entity)
        {
            await _orderDal.Insert(entity);
        }

        public async Task<List<Order>> LastFiveOrder()
        {
            return await _orderDal.LastFiveOrder();
        }

        public async Task Update(Order entity)
        {
            await _orderDal.Update(entity);
        }
    }
}
