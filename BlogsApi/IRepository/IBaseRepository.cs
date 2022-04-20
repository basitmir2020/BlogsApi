using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogsApi.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync(
            Expression<Func<T,bool>> expression = null,
            Func<IQueryable<T>,IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null);
        Task<T> GetAsync(Expression<Func<T,bool>> expression,List<string> includes = null);
        Task InsertAsync(T entity);
        Task InsertRangeAsync(IEnumerable<T> entities);
        Task DeleteAsync(int id);
        void DeleteRangeAsync(IEnumerable<T> entities);
        void UpdateAsync(T entity);
    }
}
