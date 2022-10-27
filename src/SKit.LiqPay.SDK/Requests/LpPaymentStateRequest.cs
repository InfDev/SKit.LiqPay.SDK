
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Request for getting payment status.
    /// See <see href="https://www.liqpay.ua/en/documentation/api/information/status/doc">ENG</see>
    /// or <see href="https://www.liqpay.ua/documentation/api/information/status/doc">UKR</see> dоcumentation.
    /// </summary>
    /// <remarks>Запит на отримання статусу платежу.</remarks>
    public class LpPaymentStateRequest : LpBaseRequest
    {
        /// <summary>
        /// Operation type
        /// </summary>
        /// <remarks>Тип операції</remarks>
        public string Action => "status";

        /// <summary>
        /// Unique purchase ID in your store. The maximum length is 255 characters
        /// </summary>
        /// <remarks>Унікальний ID покупки у Вашому магазині. Максимальна довжина 255 символів</remarks>
        public string OrderId { get; set; } = string.Empty;
    }
}
