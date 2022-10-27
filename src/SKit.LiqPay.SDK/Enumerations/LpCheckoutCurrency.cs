
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Supported currencies by ISO 4217 for Checkout
    /// </summary>
    /// <remarks>Валюти, що підтримуються для Checkout</remarks>
    public class LpCheckoutCurrency : Enumeration
    {
        /// <summary>
        /// United States dollar of USA
        /// </summary>
        public static readonly LpCheckoutCurrency USD = new LpCheckoutCurrency(840, nameof(USD));
        /// <summary>
        /// Euro, the European Union currency.
        /// </summary>
        public static readonly LpCheckoutCurrency EUR = new LpCheckoutCurrency(978, nameof(EUR));
        /// <summary>
        /// Ukrainian hryvnia, the national currency of Ukraine
        /// </summary>
        public static readonly LpCheckoutCurrency UAH = new LpCheckoutCurrency(980, nameof(UAH));

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        protected LpCheckoutCurrency(int id, string name) : base(id, name)
        {
        }
    }
}
