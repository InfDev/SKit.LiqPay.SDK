
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// PDT Notification Listener from LiqPay (decoding parser)
    /// </summary>
    /// <remarks>Базовий прослуховувач PDT-повідомлень від LiqPay (декодуючий парсер)</remarks>
    public class LiqPayPdtListener : LiqPayBaseListener<LpPdtResponse>, ILiqPayPdtListener
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gatewayOptions"></param>
        public LiqPayPdtListener(LiqPayGatewayOptions gatewayOptions) : base(gatewayOptions) { }
    }
}
