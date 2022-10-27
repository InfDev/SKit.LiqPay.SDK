
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Supported currencies by ISO 4217 
    /// </summary>
    /// <remarks>Валюти, що підтримуються</remarks>
    public class LpSupportedCurrency : LpCheckoutCurrency
    {
        /// <summary>
        /// United States dollar of USA
        /// </summary>
        public static readonly new LpSupportedCurrency USD = new LpSupportedCurrency(840, nameof(USD));
        /// <summary>
        /// Euro, the European Union currency.
        /// </summary>
        public static readonly new LpSupportedCurrency EUR = new LpSupportedCurrency(978, nameof(EUR));
        /// <summary>
        /// Ukrainian hryvnia, the national currency of Ukraine
        /// </summary>
        public static readonly new LpSupportedCurrency UAH = new LpSupportedCurrency(980, nameof(UAH));

        /// <summary>
        /// Russian ruble
        /// </summary>
        public static readonly LpSupportedCurrency RUB = new LpSupportedCurrency(643, nameof(RUB));
        /// <summary>
        /// Belarusian rubel
        /// </summary>
        public static readonly LpSupportedCurrency BYN = new LpSupportedCurrency(933, nameof(BYN));
        /// <summary>
        /// The tenge is the currency of Kazakhstan
        /// </summary>
        public static readonly LpSupportedCurrency KZT = new LpSupportedCurrency(398, nameof(KZT));

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        protected LpSupportedCurrency(int id, string name) : base(id, name)
        {
        }
    }
}
