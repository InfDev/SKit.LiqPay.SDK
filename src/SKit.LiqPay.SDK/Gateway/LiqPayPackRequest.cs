
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Packet with packed data and signature transmitted over the network
    /// </summary>
    /// <remarks>Пакет із запакованими даними та підписом, що передається по мережі</remarks>
    public class LiqPayPackRequest
    {
        /// <summary>
        /// Json string with request/response parameters encoded by the base64 function
        /// </summary>
        /// <remarks>Json рядок з параметрами запроса/ответа закодована функцією base64</remarks>
        public string Data { get; set; } = string.Empty;
        /// <summary>
        /// Signature of data with private key
        /// </summary>
        /// <remarks>Сигнатура даних із приватним ключем</remarks>
        public string Signature { get; set; } = string.Empty;

        /// <summary>
        /// For internal purposes
        /// </summary>
        [JsonIgnore]
        public string? JsonRequest { get; set; } = string.Empty;
    }
}
