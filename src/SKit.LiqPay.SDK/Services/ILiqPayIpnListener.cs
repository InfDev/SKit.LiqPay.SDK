
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// IPN Notification Listener from LiqPay (decoding parser)
    /// </summary>
    /// <remarks>Базовий прослуховувач IPN-повідомлень від LiqPay (декодуючий парсер)</remarks>
    public interface ILiqPayIpnListener : ILiqPayBaseListener<LpIpnResponse>
    {
    }
}

