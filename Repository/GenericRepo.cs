using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskBackend.Data;

namespace TaskBackend.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly ApplicationDbContext context;
        private readonly DbSet<T> table;

        public GenericRepo(ApplicationDbContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }

        public async Task DeleteAsync(object id)
        {
            var existing = await table.FindAsync(id);
            if (existing != null)
            {
                table.Remove(existing);
                await SaveAsync();
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = table;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }
        public async Task<T> GetByIdAsync(object id)
        {
            return await table.FindAsync(id);
        }
        public async Task InsertAsync(T obj)
        {
            await table.AddAsync(obj);
            await SaveAsync();
        }
        public async Task UpdateAsync(T obj)
        {
            table.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            await SaveAsync();
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
