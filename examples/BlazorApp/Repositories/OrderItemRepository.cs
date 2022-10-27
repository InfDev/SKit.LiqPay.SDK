using BlazorApp.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace BlazorApp.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly SqliteConnection _db;
        
        public OrderItemRepository(SqliteConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _db.QueryAsync<OrderItem>("SELECT * FROM OrderItems");
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId)
        {
            return await _db.QueryAsync<OrderItem>(
                "SELECT * FROM OrderItems WHERE OrderId = @orderId", new { orderId });
        }

        public async Task<OrderItem?> GetAsync(int id)
        {
            return (await _db.QueryAsync<OrderItem>(
                "SELECT * FROM OrderItems WHERE OrderItemId = @id", new { id }))
                .FirstOrDefault();
        }

        public async Task<OrderItem> InsertAsync(OrderItem entity)
        {
            var sql = $@"INSERT INTO OrderItems
(OrderId, Name, Quantity, Price)
VALUES(@OrderId, @Name, @Quantity, @Price);
SELECT * FROM OrderItems WHERE ROWID = last_insert_rowid();";
            var insertedOrderItem = await _db.QuerySingleAsync<OrderItem>(sql, param: entity);
            return insertedOrderItem;
            //await _db.InsertAsync<OrderItem>(entity);
        }

        public async Task<bool> UpdateAsync(OrderItem entity)
        {
            var sql = $@"UPDATE OrderItems SET
OrderId = @OrderId,
Name = @Name,
Quantity = @Quantity,
Price = @Price
WHERE OrderItemId = @OrderItemId
";
            return (await _db.ExecuteAsync(sql, entity)) == 1;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return (await _db.ExecuteAsync("DELETE FROM OrderItems WHERE OrderItemId = @id", new { id })) == 1;
        }
    }
}
