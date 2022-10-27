
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// The gateway implements low-level operations for negotiating data exchange formats with LiqPay
    /// </summary>
    /// <remarks>Шлюз реалізує низькорівневі операції для узгодження форматів обміну даними з LiqPay</remarks>
    public class LiqPayGateway : LiqPayGatewayBase, ILiqPayGateway
    {
        #region Consts
        private const bool DefaultThrowsIfNotSuccessHttpStatusCode = true;
        private const string NotSupportedLastProperties = "Not supported, set LiqPayGatewayOptions.SupportLastProperties=true";
        #endregion

        #region Fields
        private readonly HttpClient _httpClient;

        private string? _lastJsonRequest = null;
        private string? _lastJsonResponse = null;
        private HttpResponseMessage? _lastFullResponseString = null;
        #endregion

        #region Ctor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="gatewayOptions"></param>
        public LiqPayGateway(HttpClient httpClient, LiqPayGatewayOptions gatewayOptions) : base(gatewayOptions)
        {
            _httpClient = httpClient;

            // Set timeout
            var seconds = GatewayOptions.TimeoutSec ?? 0;
            if (seconds <= 0)
                seconds = 100;
            else if (seconds < 60)
                seconds = 60;
            _httpClient.Timeout = TimeSpan.FromSeconds(seconds);

            // Set base address
            _httpClient.BaseAddress = new Uri(GatewayOptions.BaseApiUrl);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Throws if not 200-299
        /// </summary>
        public bool ThrowsIfNotSuccessHttpStatusCode { get; set; } = DefaultThrowsIfNotSuccessHttpStatusCode;
        #endregion

        #region Implementation interface ILiqPayGateway

        /// <inheritdoc/>
        public async Task<string?> SendAsync<TRequest>(TRequest request, bool serverServerModel = false)
            where TRequest : LpBaseRequest
        {
            _lastJsonRequest = null;
            _lastJsonResponse = null;
            _lastFullResponseString = null;

            request.Validate();
            var pack = CreatePackage(request)!;
            if (GatewayOptions.SupportLastProperties) _lastJsonRequest = pack.JsonRequest;

            var reqContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("data", pack.Data),
                    new KeyValuePair<string, string>("signature", pack.Signature)
                });

            var uri = GetСlarifiedUri(serverServerModel);
            try
            {
                var response = await _httpClient.PostAsync(uri, reqContent);
                if (response != null)
                {
                    if (GatewayOptions.SupportLastProperties) _lastFullResponseString = response;
                    if (ThrowsIfNotSuccessHttpStatusCode)
                        response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    if (GatewayOptions.SupportLastProperties) _lastJsonResponse = content;
                    return content;
                }
            }
            catch
            {
                throw;
            }
            return null;
        }

        /// <inheritdoc/>
        public string LastJsonRequest => GatewayOptions.SupportLastProperties ? 
            _lastJsonRequest ?? "{}" : throw new Exception(NotSupportedLastProperties);

        /// <inheritdoc/>
        public string LastJsonResponse => GatewayOptions.SupportLastProperties ?
            _lastJsonResponse ?? "{}" : throw new Exception(NotSupportedLastProperties);

        /// <inheritdoc/>
        public string LastFullResponseString => GatewayOptions.SupportLastProperties ?
            _lastFullResponseString?.ToString() ?? "" : throw new Exception(NotSupportedLastProperties);
   
        #endregion

        #region Utils
        /// <summary>
        /// Returns the Uri corresponding to the call model
        /// </summary>
        /// <param name="serverServerModel"></param>
        /// <returns></returns>
        private Uri? GetСlarifiedUri(bool serverServerModel)
        {
            var uri = _httpClient.BaseAddress?.Append(
                serverServerModel ? GatewayOptions.ServerServerModelPath : GatewayOptions.ClientServerModelPath);
            return uri;
        }
        #endregion

    }
}
