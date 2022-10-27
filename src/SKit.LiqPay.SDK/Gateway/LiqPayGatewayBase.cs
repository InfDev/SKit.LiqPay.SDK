using SKit.LiqPay.SDK.Converters;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// The gateway base prepares data for low-level operations without using HttpClient.
    /// Resolves socket exhaustion when there is no need to send packets, 
    /// such as when generating a form with a button.
    /// </summary>
    /// <remarks>
    /// База шлюзу готує дані для низькорівневих операцій без використання HttpClient.
    /// Вирішує проблему вичерпання сокетів, коли немає необхідності у надсиланні пакетів,
    /// наприклад, при генерації форми з кнопкою.
    /// </remarks>
    public class LiqPayGatewayBase : ILiqPayGatewayBase
    {
        #region Consts
        private const string SandboxPrefix = "sandbox_";
        #endregion

        #region Fields
        private readonly LiqPayGatewayOptions _gatewayOptions;
        private static JsonSerializerOptions _jsonSerializerOptions => new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            PropertyNamingPolicy = new JsonLowerSnakeCaseNamingPolicy(),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            Converters = {
                new EnumStringConverterFactory(),
                new JsonDateTimeUtcConverter(),
                // new JsonUnixEpochMsConverter(),
                new JsonEnumerationConverterFactory()
            }
        };
        #endregion

        #region Ctor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gatewayOptions"></param>
        public LiqPayGatewayBase(LiqPayGatewayOptions gatewayOptions)
        {
            _gatewayOptions = gatewayOptions;
            if (string.IsNullOrWhiteSpace(_gatewayOptions.ShopCallback.PdtUrl))
                _gatewayOptions.ShopCallback.PdtUrl = null;
            if (string.IsNullOrWhiteSpace(_gatewayOptions.ShopCallback.IpnUrl))
                _gatewayOptions.ShopCallback.IpnUrl = null;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gateway options
        /// </summary>
        protected LiqPayGatewayOptions GatewayOptions => _gatewayOptions;

        #endregion

        #region Implementation interface ILiqPayGatewayBase

        /// <inheritdoc/>
        public static JsonSerializerOptions JsonSerializerOptions => _jsonSerializerOptions;

        /// <inheritdoc/>
        public LiqPayPackRequest CreatePackage<T>(T request) where T : LpBaseRequest
        {
            request.Version = LiqPayConsts.ApiVersion;
            request.PublicKey = GetСlarifiedKey(_gatewayOptions.PublicKey);
            if (request is LpCheckoutRequest)
                request.ResultUrl = _gatewayOptions.ShopCallback.PdtUrl;
            else
                request.ServerUrl = _gatewayOptions.ShopCallback.IpnUrl;
            var json = JsonSerializer.Serialize(request, _jsonSerializerOptions);
            var data = json.ToBase64String();
            return new LiqPayPackRequest
            {
                Data = data,
                Signature = LiqPayHelper.CreateSignature(data, _gatewayOptions.PrivateKey),
                JsonRequest = json
            };
        }

        #endregion

        #region Utils
        /// <summary>
        /// Checks and changes, if necessary, the key according to the sandbox mode (UseSandbox property)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetСlarifiedKey(string key)
        {
            if (key == null)
                return null!;
            if (!_gatewayOptions.UseSandbox)
                return key.Replace(SandboxPrefix, "");
            if (!key.StartsWith(SandboxPrefix, StringComparison.OrdinalIgnoreCase))
                return SandboxPrefix + key;
            return key;
        }
        #endregion
    }
}
