
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Final payment statuses.
    /// See <see href="https://www.liqpay.ua/documentation/api/information/status/doc">Dоcumentation</see>
    /// </summary>
    /// <remarks>Кінцеві статуси платежу</remarks>
    public class LpPaymentStatus : Enumeration
    {
        /// <summary>
        /// Undefined
        /// </summary>
        public static readonly LpPaymentStatus None = new LpPaymentStatus(1, "");

        // Final payment statuses 

        /// <summary>
        /// Неуспішний платіж. Некоректно заповнені дані
        /// </summary>
        public static readonly LpPaymentStatus Error = new LpPaymentStatus(1, "error");

        /// <summary>
        /// Неуспішний платіж
        /// </summary>
        public static readonly LpPaymentStatus Failure = new LpPaymentStatus(2, "failure");

        /// <summary>
        /// Платіж повернений
        /// </summary>
        public static readonly LpPaymentStatus Reversed = new LpPaymentStatus(3, "reversed");

        /// <summary>
        /// Підписка успішно оформлена
        /// </summary>
        public static readonly LpPaymentStatus Subscribed = new LpPaymentStatus(4, "subscribed");

        /// <summary>
        /// Успішний платіж
        /// </summary>
        public static readonly LpPaymentStatus Success = new LpPaymentStatus(5, "success");

        /// <summary>
        /// Підписка успішно деактивована
        /// </summary>
        public static readonly LpPaymentStatus Unsubscribed = new LpPaymentStatus(6, "unsubscribed");


        /// <summary>
        /// 
        /// </summary>
        public static LpPaymentStatus[] FinalStatuses = new LpPaymentStatus[]
        {
            Error, Failure, Reversed, Subscribed, Success
        };


        // Statuses requiring payment confirmation

        /// <summary>
        /// Потрібна 3DS верифікація.
        /// Для завершення платежу, потрібно виконати 3ds_verify.
        /// </summary>
        public static readonly LpPaymentStatus Secure3dsVerify = new LpPaymentStatus(101, "3ds_verify");

        /// <summary>
        /// Очікується підтвердження captcha
        /// </summary>
        public static readonly LpPaymentStatus CaptchaVerify = new LpPaymentStatus(102, "captcha_verify");

        /// <summary>
        /// Потрібне введення CVV картки відправника.
        /// </summary>
        public static readonly LpPaymentStatus CvvVerify = new LpPaymentStatus(103, "cvv_verify");

        /// <summary>
        /// Очікується підтвердження дзвінком ivr.
        /// </summary>
        public static readonly LpPaymentStatus IvrVerify = new LpPaymentStatus(104, "ivr_verify");

        /// <summary>
        /// Потрібне OTP підтвердження клієнта. OTP пароль відправлений на номер телефону Клієнта.
        /// </summary>
        public static readonly LpPaymentStatus OtpVerify = new LpPaymentStatus(105, "otp_verify");

        /// <summary>
        /// Очікується підтвердження пароля додатка Приват24.
        /// </summary>
        public static readonly LpPaymentStatus PasswordVerify = new LpPaymentStatus(106, "password_verify");

        /// <summary>
        /// Очікується введення телефону клієнтом.
        /// </summary>
        public static readonly LpPaymentStatus PhoneVerify = new LpPaymentStatus(107, "phone_verify");

        /// <summary>
        /// Очікується підтвердження pin-code.
        /// </summary>
        public static readonly LpPaymentStatus PinVerify = new LpPaymentStatus(108, "pin_verify");

        /// <summary>
        /// Потрібне введення даних одержувача.
        /// </summary>
        public static readonly LpPaymentStatus ReceiverVerify = new LpPaymentStatus(109, "receiver_verify");

        /// <summary>
        /// Потрібне введення даних відправника.
        /// </summary>
        public static readonly LpPaymentStatus SenderVerify = new LpPaymentStatus(110, "sender_verify");

        /// <summary>
        /// Очікується підтвердження в додатку Privat24.
        /// </summary>
        public static readonly LpPaymentStatus SenderappVerify = new LpPaymentStatus(111, "senderapp_verify");

        /// <summary>
        /// Очікується сканування QR-коду клієнтом.
        /// </summary>
        public static readonly LpPaymentStatus WaitQr = new LpPaymentStatus(112, "wait_qr");

        /// <summary>
        /// Очікується підтвердження оплати клієнтом в додатку Privat24/SENDER.
        /// </summary>
        public static readonly LpPaymentStatus WaitSender = new LpPaymentStatus(113, "wait_sender");

        /// <summary>
        /// Очікується завершення платежу в Приват24.
        /// </summary>
        public static readonly LpPaymentStatus P24Verify = new LpPaymentStatus(114, "p24_verify");

        /// <summary>
        /// Очікується завершення платежу в гаманці MasterPass
        /// </summary>
        public static readonly LpPaymentStatus MpVerify = new LpPaymentStatus(115, "mp_verify");

        // Інші статуси платежу

        /// <summary>
        /// Очікується оплата готівкою в ТСО
        /// </summary>
        public static readonly LpPaymentStatus CashWait = new LpPaymentStatus(201, "cash_wait");

        /// <summary>
        /// Сума успішно заблокована на рахунку відправника
        /// </summary>
        public static readonly LpPaymentStatus HoldWait = new LpPaymentStatus(202, "hold_wait");

        /// <summary>
        /// Інвойс створений успішно, очікується оплата
        /// </summary>
        public static readonly LpPaymentStatus InvoiceWait = new LpPaymentStatus(203, "invoice_wait");

        /// <summary>
        /// Платіж створений, очікується його завершення відправником
        /// </summary>
        public static readonly LpPaymentStatus prepared = new LpPaymentStatus(204, "prepared");

        /// <summary>
        /// Платіж обробляється
        /// </summary>
        public static readonly LpPaymentStatus Processing = new LpPaymentStatus(205, "processing");

        /// <summary>
        /// Кошти з клієнта списані, але магазин ще не пройшов перевірку.
        /// Якщо магазин не пройде активацію протягом 90 днів, платежі будуть автоматично скасовані.
        /// </summary>
        public static readonly LpPaymentStatus WaitAccept = new LpPaymentStatus(206, "wait_accept");

        /// <summary>
        /// Не встановлений спосіб відшкодування у одержувача.
        /// </summary>
        public static readonly LpPaymentStatus WaitCard = new LpPaymentStatus(207, "wait_card");

        /// <summary>
        /// Платіж успішний, буде зарахований в щодобовій проводці.
        /// </summary>
        public static readonly LpPaymentStatus WaitCompensation = new LpPaymentStatus(208, "wait_compensation");

        /// <summary>
        /// Акредитив. Кошти з клієнта списані, очікується підтвердження доставки товару.
        /// </summary>
        public static readonly LpPaymentStatus WaitLc = new LpPaymentStatus(209, "wait_lc");

        /// <summary>
        /// Грошові кошти за платежем зарезервовані для проведення повернення за раніше поданою заявкою.
        /// </summary>
        public static readonly LpPaymentStatus WaitReserve = new LpPaymentStatus(210, "wait_reserve");

        /// <summary>
        /// Платіж на перевірці.
        /// </summary>
        public static readonly LpPaymentStatus WaitSecure = new LpPaymentStatus(211, "wait_secure");

        /// <summary>
        /// Оплата неуспішна. Клієнт може повторити спробу ще раз.
        /// </summary>
        public static readonly LpPaymentStatus TryAgain = new LpPaymentStatus(212, "try_againe");

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        protected LpPaymentStatus(int id, string name) : base(id, name)
        {
        }
    }
}
