using System.Linq.Expressions;

namespace EBusinessData.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        Task AddAsync(T entity);
        Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(int? id);
        Task<T> UpdatedAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
