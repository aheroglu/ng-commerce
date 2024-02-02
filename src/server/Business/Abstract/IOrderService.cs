using Business.Abstract.Generic;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService : IGenericService<Order>
    {
        Task<List<Order>> GetOrdersWithRelations();
        Task<List<Order>> GetOrdersByUser(int userId);
        Task<Order> GetOrderDetailsByOrder(int orderNo);
        Task<int> CountOfOrders();
        Task<List<Order>> LastFiveOrder();
    }
}
