using DataAccess.Abstract.Generic;
using Entity.Concrete;

namespace DataAccess.Abstract
{
    public interface IMailDal : IGenericDal<Mail>
    {
        Task<List<Mail>> MailsByMembership(string membership);
    }
}
