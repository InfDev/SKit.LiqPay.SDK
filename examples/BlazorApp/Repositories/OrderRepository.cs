using BlazorApp.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace BlazorApp.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SqliteConnection _db;
        private IOrderItemRepository _orderItemRepositiry;

        public OrderRepository(SqliteConnection db, IOrderItemRepository orderItemRepositiry)
        {
            _db = db;
            _orderItemRepositiry = orderItemRepositiry;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _db.QueryAsync<Order>("SELECT * FROM Orders");
        }

        public async Task<Order?> GetLastAsync()
        {
            var maxId = Convert.ToInt32(await _db.ExecuteScalarAsync("SELECT MAX(OrderId) FROM Orders"));
            return await GetAsync(maxId);
        }

        public async Task<Order?> GetAsync(int id)
        {
            var order =  (await _db.QueryAsync<Order>(
                "SELECT * FROM Orders WHERE OrderId = @id", new { id }))
                .FirstOrDefault();
            if (order != null)
            {
                var items = await _orderItemRepositiry.GetOrderItemsAsync(id);
                if (items != null)
                    order.Items = items;
            }
            return order;
        }

        public async Task<Order> InsertAsync(Order entity)
        {
            entity.CreateAt = DateTime.UtcNow;
            entity.ModifyAt = entity.CreateAt;
            var sql = $@"INSERT INTO Orders
(BuyerEmail, BuyerPhone, PaymentStatus, LiqPayPaymentStatus, CreateAt, ModifyAt)
VALUES(@BuyerEmail, @BuyerPhone, @PaymentStatus, @LiqPayPaymentStatus, @CreateAt, @ModifyAt);
SELECT * FROM Orders WHERE ROWID = last_insert_rowid();";
            var insertedOrder = await _db.QuerySingleAsync<Order>(sql, param: entity);
            return insertedOrder;
        }

        public async Task<bool> UpdateAsync(Order entity)
        {
            entity.ModifyAt = DateTime.UtcNow;
            var sql = $@"UPDATE Orders SET
BuyerEmail = @BuyerEmail,
BuyerPhone = @BuyerPhone,
PaymentStatus = @PaymentStatus,
LiqPayPaymentStatus = @LiqPayPaymentStatus,
ModifyAt = @ModifyAt
WHERE OrderId = @OrderId
";
            return (await _db.ExecuteAsync(sql, entity)) == 1;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return (await _db.ExecuteAsync("DELETE FROM Orders WHERE OrderId = @id", new { id })) == 1;
        }

        public async Task ClearAllAsync()
        {
            await _db.ExecuteScalarAsync("DELETE FROM Orders;");
        }

    }
}
