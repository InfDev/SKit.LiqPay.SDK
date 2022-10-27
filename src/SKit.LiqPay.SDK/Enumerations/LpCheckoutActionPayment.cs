
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Supported action for Checkout
    /// </summary>
    /// <remarks>Типи операції, що підтримуються при Checkout</remarks>
    public class LpCheckoutActionPayment : Enumeration
    {
        /// <summary>
        /// Direct debit from the card
        /// </summary>
        /// <remarks>Пряме списання з картки</remarks>
        public static readonly LpCheckoutActionPayment Pay = new LpCheckoutActionPayment(1, "pay");
        /// <summary>
        /// Blocking funds on the client's card as part of two-stage payment
        /// </summary>
        /// <remarks>Блокування коштів на картці клієнта в рамках двостадійної оплати</remarks>
        public static readonly LpCheckoutActionPayment Hold = new LpCheckoutActionPayment(2, "hold");
        /// <summary>
        /// Subscribing
        /// </summary>
        /// <remarks>Оформлення підписки</remarks>
        public static readonly LpCheckoutActionPayment Subscribe = new LpCheckoutActionPayment(2, "subscribe");
        /// <summary>
        /// Accepting donations with any amount
        /// </summary>
        /// <remarks>Прийом пожертвувань з довільною сумою</remarks>
        public static readonly LpCheckoutActionPayment Paydonate = new LpCheckoutActionPayment(1, "paydonate");
        /// <summary>
        /// Card pre-authorization
        /// </summary>
        /// <remarks>Предавторизація картки</remarks>
        public static readonly LpCheckoutActionPayment Auth = new LpCheckoutActionPayment(1, "auth");

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        protected LpCheckoutActionPayment(int id, string name) : base(id, name)
        {
        }
    }
}
