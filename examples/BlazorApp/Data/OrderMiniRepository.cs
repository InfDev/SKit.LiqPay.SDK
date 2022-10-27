using BlazorApp.Models;

namespace BlazorApp.Data
{
    public class OrderMiniRepository
    {
        public static Order TestOrder => new Order
        {
            OrderId = 305,
            Items = new List<OrderItem> {
                new OrderItem
                {
                    OrderItemId = 1027,
                    Name = "Блок паперу 85х85 мм 500 аркушів білий проклеєний",
                    Price = 39,
                    Quantity = 1
                }
            }
        };
    }
}
