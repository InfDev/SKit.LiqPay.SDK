
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Товар для виставлення рахунку
    /// </summary>
    public class InvoiceGood
    {
        /// <summary>
        /// Найменування
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Кількість
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Одиниця виміру, наприклад 'шт.'
        /// </summary>
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// Сума
        /// </summary>
        public double Amount { get; set; }
    }
}
