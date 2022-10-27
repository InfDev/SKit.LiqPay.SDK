
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Дані для фіскалізації.
    /// </summary>
    public class RroInfo
    {
        /// <summary>
        /// Дані про товари, за які здійснюється оплата.
        /// </summary>
        public IList<RroGoodSummary>? Goods { get; set; }
        public RroDelivery? Delivery { get; set; }

    }
}
