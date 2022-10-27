
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// IPN Notification Listener from LiqPay (decoding parser)
    /// </summary>
    /// <remarks>Базовий прослуховувач IPN-повідомлень від LiqPay (декодуючий парсер)</remarks>
    public class LiqPayIpnListener : LiqPayBaseListener<LpIpnResponse>, ILiqPayIpnListener
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gatewayOptions"></param>
        public LiqPayIpnListener(LiqPayGatewayOptions gatewayOptions) : base(gatewayOptions) { }
    }
}
