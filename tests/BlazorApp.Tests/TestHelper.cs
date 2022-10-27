using BlazorApp.Models;
using System.Linq.Expressions;
using System.Text.Json;

namespace BlazorApp.Tests
{
    public static class TestHelper
    {
        private static Random _rnd = new Random(DateTime.Now.Millisecond);

        public static Order NewOrder()
        {
            return new Order
            {
                BuyerEmail = $"buyer{_rnd.Next(999)}@domen.ua",
                BuyerPhone = $"+380 99 999-99-{_rnd.Next(99):D2}"
            }; 
        }

        public static OrderItem NewOrderItem(int orderId = 0)
        {
            return new OrderItem
            {
                OrderId = orderId,
                Name = $"Product {_rnd.Next(99999):D5}",
                Quantity = _rnd.Next(1, 3),
                Price = _rnd.Next(1, 9)
            };
        }
    }
}
