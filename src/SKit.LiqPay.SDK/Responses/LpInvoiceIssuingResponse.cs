
namespace SKit.LiqPay.SDK
{
    public class LpInvoiceIssuingResponse
    {
        /// <summary>
        /// Transaction type. Possible values:
        /// <list type="bullet">
        /// <item>pay - payment></item>
        /// <item>hold - amount of hold on sender's account</item>
        /// <item>paysplit - splitting payments</item>
        /// <item>subscribe - creation of a regular payment</item>
        /// <item>auth - card preauth</item>
        /// <item>regular - regular payment</item>
        /// </list>
        /// </summary>
        /// <remarks>Тип операції. Можливі значення:
        /// 'pay' - платіж, 'hold' - блокування коштів на рахунку відправника,
        /// 'paysplit' - розщеплення платежу, 'subscribe' - створення регулярного платежу,
        /// 'paydonate' - пожертвування, 'auth' - предавторізація картки,
        /// 'regular' - регулярний платіж</remarks>
        public LpActionPayment Action { get; set; } = LpActionPayment.None;
        /// <summary>
        /// Payment amount
        /// </summary>
        /// <remarks>Сума платежу</remarks>
        public double Amount { get; set; }
        /// <summary>
        /// Currency type
        /// </summary>
        /// <remarks>Тип валюти</remarks>
        public LpSupportedCurrency? Currency { get; set; }
        /// <summary>
        /// Purpose of payment
        /// </summary>
        /// <remarks>Призначення платежу</remarks>
        public string? Description { get; set; }
        /// <summary>
        /// Link to invoice
        /// </summary>
        /// <remarks>Посилання на інвойс</remarks>
        public string? Href { get; set; } = string.Empty;
        /// <summary>
        /// Payment id in LiqPay system
        /// </summary>
        /// <remarks>Id платежу в системі LiqPay</remarks>
        public long Id { get; set; }
        /// <summary>
        /// Unique purchase ID in your store. The maximum length is 255 characters
        /// </summary>
        /// <remarks>Унікальний ID покупки у Вашому магазині. Максимальна довжина 255 символів</remarks>
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Receive channel type
        /// </summary>
        /// <remarks>Вид каналу отримання</remarks>
        public string? ReceiverType { get; set; } = string.Empty;
        /// <summary>
        /// The value obtained in the parameter ReceiverType
        /// </summary>
        /// <remarks>Значення отримане в параметрі ReceiverType</remarks>
        public string? ReceiverValue { get; set; } = string.Empty;
        public LpResult? Result { get; set; }
        /// <summary>
        /// Payment status. Available values:
        /// <list type="bullet">
        /// <item>error - Failed payment. Data is incorrect</item>
        /// <item>failure - Failed payment</item>
        /// <item>success - Successful payment</item>
        /// <item>invoice_wait - Successful payment</item>
        /// </list>
        /// </summary>
        /// <remarks>Статус платежу. Можливі значення:
        /// 'error' - неуспішний платіж. Некоректно заповнені дані, 'failure' -	неуспішний платіж,
        /// 'success' - успішний платіж, 'invoice_wait' - інвойс створений успішно, очікується оплата
        /// </remarks>
        public string Status { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;

    }
}
