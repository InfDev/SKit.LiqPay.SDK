
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Supported type of transaction when paying for a purchase
    /// </summary>
    /// <remarks>Типи операції, що підтримуються при оплаті покупки</remarks>
    public class LpActionPayment : Enumeration
    {
        public static readonly LpActionPayment None = new LpActionPayment(0, "");
        /// <summary>
        /// Direct debit from the card
        /// </summary>
        /// <remarks>Пряме списання з картки</remarks>
        public static readonly LpActionPayment Pay = new LpActionPayment(1, "pay");
        /// <summary>
        /// Blocking funds on the client's card as part of two-stage payment
        /// </summary>
        /// <remarks>Блокування коштів на картці клієнта в рамках двостадійної оплати</remarks>
        public static readonly LpActionPayment Hold = new LpActionPayment(2, "hold");
        /// <summary>
        /// Subscribing
        /// </summary>
        /// <remarks>Оформлення підписки</remarks>
        public static readonly LpActionPayment Subscribe = new LpActionPayment(2, "subscribe");
        /// <summary>
        /// Accepting donations with any amount
        /// </summary>
        /// <remarks>Прийом пожертвувань з довільною сумою</remarks>
        public static readonly LpActionPayment Paydonate = new LpActionPayment(1, "paydonate");
        /// <summary>
        /// Card pre-authorization
        /// </summary>
        /// <remarks>Предавторизація картки</remarks>
        public static readonly LpActionPayment Auth = new LpActionPayment(1, "auth");
        /// <summary>
        /// Payment with buyer protection
        /// </summary>
        /// <remarks>Платіж із захистом покупця</remarks>
        public static readonly LpActionPayment LetterOfCredit = new LpActionPayment(1, "letter_of_credit");
        /// <summary>
        /// Splitting the payment to several recipients
        /// </summary>
        /// <remarks>Розщеплення платежу на декількох одержувачів</remarks>
        public static readonly LpActionPayment SplitRules = new LpActionPayment(1, "split_rules");
        /// <summary>
        /// Pay with Apple Pay
        /// </summary>
        /// <remarks>Оплата за допомогою Apple Pay</remarks>
        public static readonly LpActionPayment Apay = new LpActionPayment(1, "apay");
        /// <summary>
        /// Pay with Google Pay
        /// </summary>
        /// <remarks>Оплата за допомогою Google Pay</remarks>
        public static readonly LpActionPayment Gpay = new LpActionPayment(1, "gpay");

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        protected LpActionPayment(int id, string name) : base(id, name)
        {
        }
    }
}
