using BlazorApp.Models;
using BlazorApp.Repositories;

namespace BlazorApp.Services
{
    public class OrderManager
    {
        private static Random _rnd = new Random(DateTime.Now.Millisecond);
        private IOrderRepository _orderRepositiry;
        private IOrderItemRepository _orderItemRepositiry;
        private static Order? _currentOrder = null;

        public OrderManager(IOrderRepository orderRepositiry, IOrderItemRepository orderItemRepositiry)
        {
            _orderRepositiry = orderRepositiry;
            _orderItemRepositiry = orderItemRepositiry;
        }

        public Order? CurrentOrder { 
            get => _currentOrder;
            set {
                _currentOrder = value;
                NotifyStateChanged();
            }
        }

        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _orderRepositiry.GetAllAsync();
        }

        public async Task<Order?> GetLastOrder()
        {
            return await _orderRepositiry.GetLastAsync();
        }


        public async Task<Order> CreateNewOrder()
        {
            var order = new Order
            {
                BuyerEmail = $"buyer{_rnd.Next(999)}@domen.ua",
                BuyerPhone = $"+380 99 999-99-{_rnd.Next(99):D2}"
            };
            order = await _orderRepositiry.InsertAsync(order);

            var orderItem = new OrderItem
            {
                OrderId = order!.OrderId,
                Name = $"Product {_rnd.Next(99999):D5}",
                Quantity = _rnd.Next(1, 3),
                Price = _rnd.Next(1, 9)
            };
            orderItem = await _orderItemRepositiry.InsertAsync(orderItem);

            var createdOrder = await _orderRepositiry.GetAsync(orderItem.OrderId);
            return createdOrder!;
        }

        public async Task<Order?> FindOrder(int orderId)
        {
            return await _orderRepositiry.GetAsync(orderId);
        }
        public async Task<bool> UpdateOrder(Order order)
        {
            var ok = await _orderRepositiry.UpdateAsync(order);
            if (order?.OrderId == _currentOrder?.OrderId)
            {
                var updatedOrder = await _orderRepositiry.GetAsync(order!.OrderId);
                CurrentOrder = updatedOrder;
                NotifyStateChanged();
            }
            return ok;
        }
    }
}
