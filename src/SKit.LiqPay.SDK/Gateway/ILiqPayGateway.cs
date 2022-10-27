
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// The gateway implements low-level operations for negotiating data exchange formats with LiqPay
    /// </summary>
    /// <remarks>Шлюз реалізує низькорівневі операції для узгодження форматів обміну даними з LiqPay</remarks>
    public interface ILiqPayGateway : ILiqPayGatewayBase
    {
        /// <summary>
        /// Sending requests to services of LiqPay
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <param name="serverServerModel"></param>
        /// <returns></returns>
        Task<string?> SendAsync<TRequest>(TRequest request, bool serverServerModel = true) where TRequest : LpBaseRequest;

        /// <summary>
        /// Returns the last sent request in json from the current Gateway instance for test purposes
        /// </summary>
        /// <returns>Last request to json before converting to package</returns>
        /// <remarks>Останній запит у json перед перетворенням на пакет</remarks>
        string LastJsonRequest { get; }
        /// <summary>
        /// Returns the last on SendAsync response content from the current Gateway instance for test purposes
        /// </summary>
        /// <remarks>Останній на SendAsync контент відповіді у json</remarks>
        string LastJsonResponse { get; }
        /// <summary>
        /// Returns the last on SendAsync response in json (headers) before content is read from the current gateway instance for test purposes.
        /// </summary>
        /// <returns>Last on SendAsync response in json (headers) before content is read</returns>
        /// <remarks>Остання на SendAsync відповідь у json (headers) до читання контенту</remarks>
        string LastFullResponseString { get; }

    }
}
