using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class MailManager : IMailService
    {
        private readonly IMailDal _mailDal;

        public MailManager(IMailDal mailDal)
        {
            _mailDal = mailDal;
        }

        public async Task Delete(Mail entity)
        {
            await _mailDal.Delete(entity);
        }

        public async Task<List<Mail>> GetAll()
        {
            return await _mailDal.GetAll();
        }

        public async Task<List<Mail>> GetAll(Expression<Func<Mail, bool>> filter)
        {
            return await _mailDal.GetAll(filter);
        }

        public async Task<Mail> GetById(int id)
        {
            return await _mailDal.GetById(id);
        }

        public async Task Insert(Mail entity)
        {
            await _mailDal.Insert(entity);
        }

        public async Task<List<Mail>> MailsByMembership(string membership)
        {
            return await _mailDal.MailsByMembership(membership);
        }

        public async Task Update(Mail entity)
        {
            await _mailDal.Update(entity);
        }
    }
}
