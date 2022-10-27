
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Payment method
    /// </summary>
    /// <remarks>Спосіб оплати</remarks>
    public class LpPaymentMethod : Enumeration
    {
        /// <summary>
        /// Оплата картою.
        /// </summary>
        public static readonly LpPaymentMethod Card = new LpPaymentMethod(1, "card");

        /// <summary>
        /// Через кабінет liqpay.
        /// </summary>
        public static readonly LpPaymentMethod Liqpay = new LpPaymentMethod(2, "liqpay");

        /// <summary>
        /// Через кабінет Приват24.
        /// </summary>
        public static readonly LpPaymentMethod Privat24 = new LpPaymentMethod(3, "privat24");

        /// <summary>
        /// Розстрочка.
        /// </summary>
        public static readonly LpPaymentMethod MomentPart = new LpPaymentMethod(4, "moment_part");

        /// <summary>
        /// Готівкою.
        /// </summary>
        public static readonly LpPaymentMethod Cash = new LpPaymentMethod(5, "cash");

        /// <summary>
        /// Рахунок на e-mail.
        /// </summary>
        public static readonly LpPaymentMethod Invoice = new LpPaymentMethod(6, "invoice");

        /// <summary>
        /// Сканування QR-коду.
        /// </summary>
        public static readonly LpPaymentMethod Qr = new LpPaymentMethod(7, "qr");


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        protected LpPaymentMethod(int id, string name) : base(id, name)
        {
        }
    }
}
