
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Сумарно по товару для чека
    /// </summary>
    public class RroGoodSummary
    {
        /// <summary>
        /// Дані товару.
        /// </summary>
        public RroGood Good { get; set; } = new RroGood();
        /// <summary>
        /// Number	Кількість. Наприклад: 1 шт. = 1000, 2.25 кг = 2250.
        /// </summary>
        public double Quantity { get; set; }
        /// <summary>
        /// Здійснюється повернення товару. Опционально
        /// </summary>
        public bool? IsReturn { get; set; }

    }
}
