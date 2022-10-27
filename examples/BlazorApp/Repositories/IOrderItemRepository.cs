using BlazorApp.Models;

namespace BlazorApp.Repositories
{
    public interface IOrderItemRepository : IBaseRepository<OrderItem>
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId);
    }
}
