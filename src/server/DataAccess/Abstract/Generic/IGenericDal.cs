using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.Generic
{
    public interface IGenericDal<T> where T : class
    {
        Task Insert(T entity);
        Task Delete(T entity);
        Task Update(T entity);
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(Expression<Func<T, bool>> filter);
        Task<T> GetById(int id);
    }
}
