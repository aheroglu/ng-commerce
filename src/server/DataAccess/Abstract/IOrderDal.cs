using DataAccess.Abstract.Generic;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IOrderDal : IGenericDal<Order>
    {
        Task<List<Order>> GetOrdersWithRelations();
        Task<List<Order>> GetOrdersByUser(int userId);
        Task<Order> GetOrderDetailsByOrder(int orderNo);
        Task<int> CountOfOrders();
        Task<List<Order>> LastFiveOrder();
    }
}
