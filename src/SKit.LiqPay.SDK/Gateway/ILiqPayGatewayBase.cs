
namespace SKit.LiqPay.SDK
{
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

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
    public interface ILiqPayGatewayBase
    {
        /// <summary>
        /// Json serializer options for Gateway
        /// </summary>
        static JsonSerializerOptions JsonSerializerOptions { get; }

        /// <summary>
        /// Create a packet with packed data and signature for transmission over the network
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        LiqPayPackRequest CreatePackage<T>(T request) where T : LpBaseRequest;
    }
}
