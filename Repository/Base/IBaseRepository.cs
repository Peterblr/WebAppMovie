using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebAppMovie.Repository.Base
{
    public interface IBaseRepository<T> where T : class, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(int id);


        Task AddAsync(T item);
        Task UpdateAsync(T newItem);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
