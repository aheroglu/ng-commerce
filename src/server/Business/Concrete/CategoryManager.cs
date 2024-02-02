using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public async Task Delete(Category entity)
        {
            await _categoryDal.Delete(entity);
        }

        public async Task<List<Category>> GetAll()
        {
            return await _categoryDal.GetAll();
        }

        public async Task<List<Category>> GetAll(Expression<Func<Category, bool>> filter)
        {
            return filter == null
                ? await _categoryDal.GetAll()
                : await _categoryDal.GetAll(filter);
        }

        public async Task<Category> GetById(int id)
        {
            return await _categoryDal.GetById(id);
        }

        public async Task<Category> GetByUrlHandle(string urlHandle)
        {
            return await _categoryDal.GetByUrlHandle(urlHandle);
        }

        public async Task Insert(Category entity)
        {
            await _categoryDal.Insert(entity);
        }

        public async Task Update(Category entity)
        {
            await _categoryDal.Update(entity);
        }
    }
}
