
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// PDT Notification Listener from LiqPay (decoding parser)
    /// </summary>
    /// <remarks>Базовий прослуховувач PDT-повідомлень від LiqPay (декодуючий парсер)</remarks>
    public interface ILiqPayPdtListener : ILiqPayBaseListener<LpPdtResponse>
    {
    }
}
