
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Request for send invoice.
    /// See <see href="https://www.liqpay.ua/en/documentation/api/aquiring/invoice/doc">ENG</see>
    /// or <see href="https://www.liqpay.ua/documentation/api/aquiring/invoice/doc">UKR</see> dоcumentation.
    /// </summary>
    /// <remarks>Запит на надсилання рахунку</remarks>
    public class LpInvoiceIssuingRequest : LpBaseRequest
    {
        private string _language = LiqPayConsts.DefaultLanguage;

        /// <summary>
        /// Type of transaction when paying for a purchase
        /// </summary>
        /// <remarks>Тип операції з оплати покупки</remarks>
        public string Action => "invoice_send";
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
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Код помилки
        /// </summary>
        public string? ErrCode { get; set; }
        /// <summary>
        /// Опис помилки
        /// </summary>
        public string? ErrDescription { get; set; }
        /// <summary>
        /// Unique purchase ID in your store. The maximum length is 255 characters
        /// </summary>
        /// <remarks>Унікальний ID покупки у Вашому магазині. Максимальна довжина 255 символів</remarks>
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Customer's e-mail to send invoice (phone or email required parameters for transmission)
        /// </summary>
        /// <remarks>Email клієнта для відправки інвойсу
        /// (phone або email обов'язкові параметри для передачі)</remarks>
        public string? Email { get; set; }
        /// <summary>
        /// The phone to which the invoice will be sent (phone or email required parameters for transmission)
        /// </summary>
        /// <remarks>Телефон, на який буде відправлено інвойс
        /// (phone або email обов'язкові параметри для передачі)</remarks>
        public string? Phone { get; set; }
        /// <summary>
        /// Transaction type. Possible values:
        /// <list type="bullet">
        /// <item>pay - payment</item>
        /// <item>hold - amount of hold on sender's account</item>
        /// <item>subscribe - regular payment</item>
        /// <item>paydonate - donation</item>
        /// </list>
        /// </summary>
        /// <remarks>Тип операції. Можливі значення: 'pay' - платіж,
        /// 'hold' - блокування коштів на рахунку відправника, 'subscribe' - регулярний платіж,
        /// 'paydonate' - пожертва</remarks>
        public LpInvoiceActionPayment ActionPayment { get; set; } = LpInvoiceActionPayment.Pay;
        /// <summary>
        /// Date and time untill which customer is able to pay invoice by UTC.
        /// Should be sent in the following format YYYY-MM-DD HH:mm:ss
        /// </summary>
        /// <remarks>Час до якого клієнт може оплатити рахунок за UTC. 
        /// Передається в форматі YYYY-MM-DD HH:mm:ss</remarks>
        public DateTime? ExpiredDate { get; set; }
        /// <summary>
        /// Goods
        /// </summary>
        /// <remarks>Товари</remarks>
        public IList<InvoiceGood>? Goods { get; set; }
        /// <summary>
        /// Two-letter code according to standard ISO 639-1. Optional
        /// </summary>
        /// <remarks>Двобуквений код згідно стандарту ISO 639-1. Необов'язково</remarks>
        public string Language
        {
            get => _language;
            set => _language = LiqPayHelper.ToSupportedLanguageForClient(value);
        }

    }
}
