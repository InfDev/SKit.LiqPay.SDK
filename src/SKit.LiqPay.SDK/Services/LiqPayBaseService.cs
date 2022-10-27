
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// The base service allows you to access the gateway only
    /// </summary>
    /// <remarks>Базовий сервіс дозволяє отримати доступ лише до шлюзу</remarks>
    public class LiqPayBaseService : ILiqPayBaseService
    {
        private ILiqPayGatewayBase _gateway;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gateway"></param>
        public LiqPayBaseService(ILiqPayGatewayBase gateway)
        {
            _gateway = gateway;
        }

        /// <inheritdoc/>
        public ILiqPayGatewayBase Gateway => _gateway;
    }
}
