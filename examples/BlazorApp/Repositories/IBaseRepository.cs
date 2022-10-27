using BlazorApp.Models;

namespace BlazorApp.Repositories
{
    public interface IBaseRepository<T> where T : IBaseEntity, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task<T> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
