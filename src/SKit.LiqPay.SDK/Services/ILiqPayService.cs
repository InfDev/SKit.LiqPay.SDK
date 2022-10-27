
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Basic service that allows you to interact with LiqPay through a personalized payment page.
    /// See <see href="https://www.liqpay.ua/en/documentation/api/aquiring/checkout">ENG</see>
    /// or <see href="https://www.liqpay.ua/documentation/api/aquiring/checkout">UKR</see> dоcumentation.
    /// </summary>
    public interface ILiqPayService : ILiqPayBaseService
    {
        /// <summary>
        /// Gateway with HttpClient
        /// </summary>
        ILiqPayGateway ClientGateway { get; }

        /// <summary>
        /// Parsing the response into a type
        /// </summary>
        /// <typeparam name="T">Type inherited from LpPaymentStateBase</typeparam>
        /// <param name="jsonContent"></param>
        /// <returns></returns>
        (bool Ok, T? obj) Parse<T>(string jsonContent) where T : LpPaymentStateBase;

        /// <summary>
        /// Making a payment through API (Server-Server).
        /// See <see href="https://www.liqpay.ua/en/documentation/api/aquiring/checkout">ENG</see>
        /// or <see href="https://www.liqpay.ua/documentation/api/aquiring/checkout">UKR</see> dоcumentation.
        /// </summary>
        /// <returns>Json content</returns>
        /// <remarks>Оформлення оплати через API (Server-Server)</remarks>
        Task<string?> CheckoutApiAsync(LpCheckoutApiRequest request);

        /// <summary>
        /// Getting payment status.
        /// See <see href="https://www.liqpay.ua/en/documentation/api/information/status">ENG</see>
        /// or <see href="https://www.liqpay.ua/documentation/api/information/status">UKR</see> dоcumentation.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Json content</returns>
        /// <remarks>Получение статуса платежа</remarks>
        Task<string?> GetPaymentStateAsync(string orderId);

        /// <summary>
        /// Issuing the invoice to the client's email in sever-server mode
        /// See <see href="https://www.liqpay.ua/en/documentation/api/aquiring/invoice/doc">ENG</see>
        /// or <see href="https://www.liqpay.ua/documentation/api/aquiring/invoice/doc">UKR</see> dоcumentation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>Виставлення рахунку на e-mail клієнта в режимі server-server</remarks>
        Task<LpInvoiceIssuingResponse?> InvoiceIssuingAsync(LpInvoiceIssuingRequest request);

        /// <summary>
        /// Invoice cancelation
        /// See <see href="https://www.liqpay.ua/en/documentation/api/aquiring/invoice_cancel/doc">ENG</see>
        /// or <see href="https://www.liqpay.ua/documentation/api/aquiring/invoice_cancel/doc">UKR</see> dоcumentation.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <remarks>Скасування інвойсу</remarks>
        Task<LpInvoiceCancelResponse?> InvoiceCancelAsync(string orderId);
    }
}
