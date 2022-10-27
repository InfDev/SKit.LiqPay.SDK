
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Basic notification listener from LiqPay (decoding parser)
    /// </summary>
    /// <remarks>Базовий прослуховувач повідомлень від LiqPay</remarks>
    public interface ILiqPayBaseListener<T>
    {
        /// <summary>
        /// Error messages
        /// </summary>
        public IList<string> Errors { get; }

        /// <summary>
        /// Parser for notification handler from LiqPay.
        /// See <see href="https://www.liqpay.ua/documentation/api/information/status">Dоcumentation</see>
        /// </summary>
        /// <param name="dictionary">Form keys-values</param>
        /// <returns></returns>
        /// <remarks>Парсер для обробки повідомлень від LiqPay</remarks>
        (bool, T?) DecodingParser(Dictionary<string, string> dictionary);

        /// <summary>
        /// Parser for notification handler from LiqPay.
        /// See <see href="https://www.liqpay.ua/documentation/api/information/status">Dоcumentation</see>
        /// </summary>
        /// <param name="data">Json data encoded by the base64 function</param>
        /// <param name="signature">Signature</param>
        /// <returns></returns>
        /// <remarks>Парсер для обробки повідомлень від LiqPay</remarks>
        (bool, T?) DecodingParser(string data, string signature);

        /// <summary>
        /// Parser for notification handler from LiqPay.
        /// See <see href="https://www.liqpay.ua/documentation/api/information/status">Dоcumentation</see>
        /// </summary>
        /// <param name="lpPack">Pack including Data and Signature</param>
        /// <returns></returns>
        /// <remarks>Парсер для обробки повідомлень від LiqPay</remarks>
        (bool, T?) DecodingParser(LiqPayPackCallback lpPack);

        /// <summary>
        /// Converting to a Typed Payment State
        /// </summary>
        /// <param name="data"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        /// <remarks>Перетворення на типізований стан платежу</remarks>
        (bool, LpPaymentStateBase?) ConvertToPaymentState(string data, string signature);
    }
}
