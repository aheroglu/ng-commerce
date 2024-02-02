using DataAccess.Abstract.Generic;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface INewsletterDal : IGenericDal<Newsletter>
    {
        Task<Newsletter> GetNewsletterByEmail(string email);
        Task<bool> IsSubscribed(string email);
        Task<int> CountOfSubscribers();
    }
}
