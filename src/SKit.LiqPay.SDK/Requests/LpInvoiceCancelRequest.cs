
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Invoice cancelation
    /// See <see href="https://www.liqpay.ua/en/documentation/api/aquiring/invoice_cancel/doc">ENG</see>
    /// or <see href="https://www.liqpay.ua/documentation/api/aquiring/invoice_cancel/doc">UKR</see> dоcumentation.
    /// </summary>
    /// <remarks>Скасування інвойсу</remarks>
    public class LpInvoiceCancelRequest : LpBaseRequest
    {
        /// <summary>
        /// Operation type
        /// </summary>
        /// <remarks>Тип операції</remarks>
        public string Action => "invoice_cancel";

        /// <summary>
        /// Unique purchase ID in your store. The maximum length is 255 characters
        /// </summary>
        /// <remarks>Унікальний ID покупки у Вашому магазині. Максимальна довжина 255 символів</remarks>
        public string OrderId { get; set; } = string.Empty;
    }
}
