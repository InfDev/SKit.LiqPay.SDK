
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// The base service allows you to access the gateway only
    /// </summary>
    /// <remarks>Базовий сервіс дозволяє отримати доступ лише до шлюзу</remarks>
    public interface ILiqPayBaseService
    {
        /// <summary>
        /// The gateway implements low-level operations for negotiating data exchange formats with LiqPay
        /// </summary>
        /// <remarks>Шлюз реалізує низькорівневі операції для узгодження форматів обміну даними з LiqPay</remarks>
        ILiqPayGatewayBase Gateway { get; }
    }
}
