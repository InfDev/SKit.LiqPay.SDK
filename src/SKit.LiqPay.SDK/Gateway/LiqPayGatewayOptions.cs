
using System.Collections.Generic;

namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Gateway Options
    /// </summary>
    /// <remarks>Параметри шлюзу</remarks>
    public class LiqPayGatewayOptions
    {
        /// <summary>
        /// Section name with options in appsettings.json
        /// </summary>
        public const string LiqPayGatewaySection = "LiqPayGateway";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="privateKey"></param>
        public LiqPayGatewayOptions(string publicKey, string privateKey)
        {
            PublicKey = publicKey;
            PrivateKey = privateKey;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public LiqPayGatewayOptions()
        { }

        /// <summary>
        /// The unique identifier of your company in the LiqPay system
        /// </summary>
        /// <remarks>Унікальний ідентифікатор Вашої компанії в системі LiqPay</remarks>
        public string PublicKey { get; set; } = string.Empty;
        /// <summary>
        /// API access secret key
        /// </summary>
        /// <remarks>Секретний ключ доступу до API</remarks>
        public string PrivateKey { get; set; } = string.Empty;

        /// <summary>
        /// API Base URL
        /// </summary>
        /// <remarks>Базова URL-адреса API</remarks>
        public string BaseApiUrl { get; set; } = LiqPayConsts.DefaultBaseApiUrl;
        /// <summary>
        /// Segments to the base URL for the Server-Server interaction model
        /// </summary>
        /// <remarks>Сегменти до базової URL-адреси для моделі взаємодії Server-Server</remarks>
        public string ServerServerModelPath { get; set; } = LiqPayConsts.DefaultServerServerModelPath;
        /// <summary>
        /// Segments to the base URL for the Client-Server interaction model
        /// </summary>
        /// <remarks>Сегменти до базової URL-адреси для моделі взаємодії Client-Server</remarks>
        public string ClientServerModelPath { get; set; } = LiqPayConsts.DefaultClientServerModelPath;
        /// <summary>
        /// Use a sandbox for testing
        /// </summary>
        /// <remarks>Використовувати пісочницю для тестування</remarks>
        public bool UseSandbox { get; set; } = false;
        /// <summary>
        /// Time to wait for a response to a request. Default 100 sec.
        /// </summary>
        /// <remarks>Час очікування відповіді на запит. За замовчуванням 100 сек</remarks>
        public int? TimeoutSec { get; set; }

        /// <summary>
        /// URLs API in your shop
        /// </summary>
        /// <remark>URLs API в Вашому магазині</remark>
        public ShopCallback ShopCallback { get; set; } = new ShopCallback();

        /// <summary>
        /// Support LiqPayGateway Last properties
        /// (LastJsonRequest, LastJsonResponse, LastFullResponseString) for logging capabilities
        /// </summary>
        /// <remarks>Підтримувати Last-властивості LiqPayGateway для можливості протоколювання</remarks> 
        public bool SupportLastProperties { get; set; } = true;
    }

    /// <summary>
    /// URLs API in your shop
    /// </summary>
    /// <remark>URs API в Вашому магазині</remark>
    public class ShopCallback
    {
        /// <summary>
        /// result_url
        /// </summary>
        public string? PdtUrl { get; set; }
        /// <summary>
        /// server_url
        /// </summary>
        public string? IpnUrl { get; set; }
    }
}
