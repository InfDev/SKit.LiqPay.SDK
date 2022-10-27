using BlazorApp.Models;

namespace BlazorApp.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task ClearAllAsync();
        Task<Order?> GetLastAsync();
    }
}
