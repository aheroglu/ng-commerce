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
    public class NewsletterManager : INewsletterService
    {
        private readonly INewsletterDal _newsletterDal;

        public NewsletterManager(INewsletterDal newsletterDal)
        {
            _newsletterDal = newsletterDal;
        }

        public async Task<int> CountOfSubscribers()
        {
            return await _newsletterDal.CountOfSubscribers();
        }

        public async Task Delete(Newsletter entity)
        {
            await _newsletterDal.Delete(entity);
        }

        public async Task<List<Newsletter>> GetAll()
        {
            return await _newsletterDal.GetAll();
        }

        public async Task<List<Newsletter>> GetAll(Expression<Func<Newsletter, bool>> filter)
        {
            return filter == null
                ? await _newsletterDal.GetAll()
                : await _newsletterDal.GetAll(filter);
        }

        public async Task<Newsletter> GetById(int id)
        {
            return await _newsletterDal.GetById(id);
        }

        public async Task<Newsletter> GetNewsletterByEmail(string email)
        {
            return await _newsletterDal.GetNewsletterByEmail(email);
        }

        public async Task Insert(Newsletter entity)
        {
            await _newsletterDal.Insert(entity);
        }

        public async Task<bool> IsSubscribed(string email)
        {
            return await _newsletterDal.IsSubscribed(email);
        }

        public async Task Update(Newsletter entity)
        {
            await _newsletterDal.Update(entity);
        }
    }
}
