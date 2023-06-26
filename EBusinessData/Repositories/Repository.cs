using EBusinessData.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace EBusinessData.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {

        private readonly AppDbContext context;

        public Repository(AppDbContext context)
        {
            this.context = context;
        }

        private DbSet<T> Table { get => context.Set<T>(); }

        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.AnyAsync(predicate);
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => Table.Remove(entity));
        }

        public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            if(predicate != null)
                query = query.Where(predicate);
            if(includeProperties.Any())
                foreach(var item in includeProperties)
                    query = query.Include(item);

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int? id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<T> UpdatedAsync(T entity)
        {
            await Task.Run(() => Table.Update(entity));
            return entity;
        }
    }
}
