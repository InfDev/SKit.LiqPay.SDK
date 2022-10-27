
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Supported action for Invoice
    /// </summary>
    /// <remarks>Типи операції, що підтримуються при Invoice</remarks>
    public class LpInvoiceActionPayment : Enumeration
    {
        /// <summary>
        /// Direct debit from the card
        /// </summary>
        /// <remarks>Пряме списання з картки</remarks>
        public static readonly LpInvoiceActionPayment Pay = new LpInvoiceActionPayment(1, "pay");
        /// <summary>
        /// Blocking funds on the client's card as part of two-stage payment
        /// </summary>
        /// <remarks>Блокування коштів на картці клієнта в рамках двостадійної оплати</remarks>
        public static readonly LpInvoiceActionPayment Hold = new LpInvoiceActionPayment(2, "hold");
        /// <summary>
        /// Subscribing
        /// </summary>
        /// <remarks>Оформлення підписки</remarks>
        public static readonly LpInvoiceActionPayment Subscribe = new LpInvoiceActionPayment(2, "subscribe");
        /// <summary>
        /// Accepting donations with any amount
        /// </summary>
        /// <remarks>Прийом пожертвувань з довільною сумою</remarks>
        public static readonly LpInvoiceActionPayment Paydonate = new LpInvoiceActionPayment(1, "paydonate");

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        protected LpInvoiceActionPayment(int id, string name) : base(id, name)
        {
        }
    }
}
