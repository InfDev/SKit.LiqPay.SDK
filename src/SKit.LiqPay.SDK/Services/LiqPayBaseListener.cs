
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Basic notification listener from LiqPay
    /// </summary>
    /// <remarks>Базовий прослуховувач повідомлень від LiqPay (декодуючий парсер)</remarks>
    public class LiqPayBaseListener<T> : ILiqPayBaseListener<T> where T : LpPaymentStateBase
    {
        private readonly LiqPayGatewayOptions _gatewayOptions;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gatewayOptions"></param>
        public LiqPayBaseListener(LiqPayGatewayOptions gatewayOptions)
        {
            _gatewayOptions = gatewayOptions;
        }

        private IList<string> _errors = new List<string>();

        /// <summary>
        /// Error messages
        /// </summary>
        public IList<string> Errors => _errors;

        /// <summary>
        /// Validate a notification using a signature
        /// </summary>
        /// <returns></returns>
        /// <remarks>Перевірка увідомлення з використанням підпису</remarks>
        private bool ValidateNotification(LiqPayPackCallback lpPack)
        {
            if (lpPack == null)
            {
                Errors.Add($"LiqPay notification pack not defined");
                return false;
            }
            if (string.IsNullOrEmpty(lpPack.Data) || string.IsNullOrEmpty(lpPack.Signature))
            {
                Errors.Add("Invalid LiqPay notification format");
                return false;
            }
            var expectedSignature = LiqPayHelper.CreateSignature(lpPack.Data, _gatewayOptions.PrivateKey);
            if (lpPack.Signature != expectedSignature)
            {
                Errors.Add("Data signatures do not match");
                return false;
            }
            return true;
        }

        /// <inheritdoc/>
        public (bool, T?) DecodingParser(Dictionary<string,string> dictionary)
        {
            Errors.Clear();
            if (dictionary == null)
            {
                Errors.Add($"Argument {nameof(dictionary)} not defined.");
                return (false, (T?)null);
            }
            try
            {
                string data = dictionary["data"];
                string signature = dictionary["signature"];
                return DecodingParser(data, signature);
            }
            catch (Exception ex)
            {
                Errors.Add($" {ex.Message}");
            }
            return (false, (T?)null);
        }

        /// <inheritdoc/>
        public (bool, T?) DecodingParser(string data, string signature)
        {
            var pack = new LiqPayPackCallback
            {
                Data = data,
                Signature = signature,
            };
            return DecodingParser(pack);
        }

        /// <inheritdoc/>
        public (bool, T?) DecodingParser(LiqPayPackCallback lpPack)
        {
            Errors.Clear();
            if (ValidateNotification(lpPack))
            {
                var jsonData = LiqPayHelper.DecodeBase64(lpPack.Data!);
                try
                {
                    var obj = JsonSerializer.Deserialize<T>(jsonData, LiqPayGatewayBase.JsonSerializerOptions);
                    return (true, obj);
                }
                catch(Exception ex)
                {
                    Errors.Add($"Error converting to {typeof(T).Name} from json: {jsonData}. {ex.Message}");
                }
            }
            return (false, (T?)null);
        }

        /// <inheritdoc/>
        public (bool, LpPaymentStateBase?) ConvertToPaymentState(string data, string signature)
        {
            return DecodingParser(data, signature);
        }

    }
}
