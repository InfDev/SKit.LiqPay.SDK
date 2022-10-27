
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Дані для доставки чека
    /// </summary>
    public class RroDelivery
    {
        /// <summary>
        /// E-mail, куди буде доставлено чек після фіскалізації.
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Номер телефону, куди буде доставлено чек після фіскалізації.
        /// </summary>
        public string? Phone { get; set; }
    }
}
