
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Basic service that allows you to interact with LiqPay through API (Server-Server).
    /// See <see href="https://www.liqpay.ua/en/documentation/api/aquiring/checkout">ENG</see>
    /// or <see href="https://www.liqpay.ua/documentation/api/aquiring/checkout">UKR</see> dоcumentation.
    /// </summary>
    /// <remarks>Базовий сервіс, який дозволяє вам взаємодіяти з LiqPay через API (Server-Server).</remarks>
    public class LiqPayService : LiqPayBaseService, ILiqPayService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gateway"></param>
        public LiqPayService(ILiqPayGateway gateway) : base(gateway) 
        {
        }

        public ILiqPayGateway ClientGateway => (ILiqPayGateway)Gateway;

        public (bool Ok, T? obj) Parse<T>(string jsonContent) where T : LpPaymentStateBase
        {
            T? obj = null;
            try
            {
                obj = JsonSerializer.Deserialize<T>(jsonContent, LiqPayGateway.JsonSerializerOptions);
            }
            catch(Exception ex)
            {
                // If there is a mismatch between types and supported values,
                // then include the json response in the exception
                // if the corresponding support is enabled (LiqPayGatewayOptions.SupportLastProperties = true)
                var message = ex.Message;
                if (!string.IsNullOrEmpty(ClientGateway.LastJsonResponse)
                        && ClientGateway.LastJsonResponse.Length > 2)
                    message += ClientGateway.LastJsonResponse;
                throw new Exception(message, ex);
            }
            return (obj != null, obj);
        }

        /// <inheritdoc/>
        public async Task<string?> CheckoutApiAsync(LpCheckoutApiRequest request)
        {
            return await ClientGateway.SendAsync(request, true);
        }

        /// <inheritdoc/>
        public async Task<string?> GetPaymentStateAsync(string orderId)
        {
            return await ClientGateway.SendAsync(new LpPaymentStateRequest
            {
                OrderId = orderId
            }, true);
        }

        /// <inheritdoc/>
        public async Task<LpInvoiceIssuingResponse?> InvoiceIssuingAsync(LpInvoiceIssuingRequest request)
        {
            var content = await ClientGateway.SendAsync(request);
            var obj = JsonSerializer.Deserialize<LpInvoiceIssuingResponse>(content!,
                        LiqPayGateway.JsonSerializerOptions);
            return obj;
        }

        /// <inheritdoc/>
        public async Task<LpInvoiceCancelResponse?> InvoiceCancelAsync(string orderId)
        {
            var content = await ClientGateway.SendAsync(new LpInvoiceCancelRequest
            {
                OrderId = orderId
            });
            var obj = JsonSerializer.Deserialize<LpInvoiceCancelResponse>(content!,
                        LiqPayGateway.JsonSerializerOptions);
            return obj;
        }
    }
}
