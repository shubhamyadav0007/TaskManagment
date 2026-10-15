using System.Linq.Expressions;

namespace TaskBackend.Repository
{
    public interface IGenericRepo<T> where T : class
    {
        Task DeleteAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(object id);
        Task InsertAsync(T obj);
        Task UpdateAsync(T obj);
        Task SaveAsync();
    }
}
