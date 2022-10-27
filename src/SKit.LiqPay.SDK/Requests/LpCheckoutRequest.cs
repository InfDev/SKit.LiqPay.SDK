
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Request for payment on the LiqPay page.
    /// See <see href="https://www.liqpay.ua/en/documentation/api/aquiring/checkout/doc">ENG</see>
    /// or <see href="https://www.liqpay.ua/documentation/api/aquiring/checkout/doc">UKR</see> dоcumentation.
    /// </summary>
    /// <remarks>Запит на оплату на сторінці LiqPay</remarks>
    public class LpCheckoutRequest : LpBaseRequest
    {
        private string _language = LiqPayConsts.DefaultLanguage;

        /// <summary>
        /// Type of transaction when paying for a purchase
        /// </summary>
        /// <remarks>Тип операції з оплати покупки</remarks>
        public LpCheckoutActionPayment Action { get; set; } = LpCheckoutActionPayment.Pay;
        /// <summary>
        /// Payment amount
        /// </summary>
        /// <remarks>Сума платежу</remarks>
        public double Amount { get; set; }
        /// <summary>
        /// Currency type
        /// </summary>
        /// <remarks>Тип валюти</remarks>
        public LpCheckoutCurrency Currency { get; set; } = LpCheckoutCurrency.UAH;
        /// <summary>
        /// Purpose of payment
        /// </summary>
        /// <remarks>Призначення платежу</remarks>
        public string Description { get; set; } = String.Empty;
        /// <summary>
        /// Unique purchase ID in your store. The maximum length is 255 characters
        /// </summary>
        /// <remarks>Унікальний ID покупки у Вашому магазині. Максимальна довжина 255 символів</remarks>
        public string OrderId { get; set; } = string.Empty;

        /// <summary>
        /// Data for fiscalization. Optional
        /// </summary>
        /// <remarks>Дані для фіскалізаці. Необов'язково</remarks>
        public RroInfo? RroInfo { get; set; }
        /// <summary>
        /// The time by which the customer can pay the bill in UTC. Transmitted in the format YYYY-MM-DD HH:mm:ss. Optional
        /// </summary>
        /// <remarks>Час до якого клієнт може оплатити рахунок за UTC. Передається в форматі YYYY-MM-DD HH:mm:ss. Необов'язково</remarks>
        public DateTime? ExpiredDate { get; set; }
        /// <summary>
        /// Two-letter code according to standard ISO 639-1. Optional
        /// </summary>
        /// <remarks>Двобуквений код згідно стандарту ISO 639-1. Необов'язково</remarks>
        public string Language {
            get => _language;
            set => _language = LiqPayHelper.ToSupportedLanguageForClient(value);
        }

        /// <summary>
        /// The parameter that transmits the payment methods that will be displayed at the checkout.
        /// Possible values:
        /// <list type="bullet">
        /// <item>apay - payment using Apple Pay</item>
        /// <item>gpay - payment using Google Pay</item>
        /// <item>card - payment by card</item>
        /// <item>liqpay - through the liqpay account</item>
        /// <item>privat24 - through the privat24 account</item>
        /// <item>masterpass - through the masterpass account</item>
        /// <item>moment_part - installment</item>
        /// <item>cash - in cash</item>
        /// <item>invoice - e-mail account</item>
        /// <item>qr - qr-code scanning</item>
        /// </list>
        /// If the parameter is not passed, then the settings in your shop's LiqPay cabinet, Checkout tab are used.
        /// </summary>
        public string? Paytypes { get; set; }

        /// <summary>
        /// Динамічний код верифікації, генерується і повертається в Callback.
        /// Так само згенерований код буде переданий в транзакції верифікації
        /// для відображення у виписці по картці клієнта. Працює для action= auth.
        /// </summary>
        public string? Verifycode { get; set; }

        /// <summary>
        /// Performs validation of a request
        /// </summary>
        /// <remarks>Здійснює перевірку запиту на допустимість</remarks>
        protected override void InternalValidate()
        {
            if (Amount == default(double))
                ProblemParameterNameList.Add(nameof(Amount));
            if (string.IsNullOrWhiteSpace(Description))
                ProblemParameterNameList.Add(nameof(Description));
            if (string.IsNullOrWhiteSpace(OrderId))
                ProblemParameterNameList.Add(nameof(OrderId));
        }

    }
}
