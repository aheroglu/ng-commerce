using Business.Abstract.Generic;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface IMailService : IGenericService<Mail>
    {
        Task<List<Mail>> MailsByMembership(string membership);
    }
}
