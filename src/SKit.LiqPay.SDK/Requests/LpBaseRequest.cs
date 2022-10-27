
using SKit.LiqPay.SDK.Exceptions;

namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Ceneric basic request.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>Універсальний базовий запит</remarks>
    public class LpBaseRequest<T> where T : LpBaseRequest
    {
    }

    /// <summary>
    /// Basic request. All properties of the base query are filled in automatically.
    /// </summary>
    /// <remarks>Базовый запрос. Усі властивості базового запиту заповнюються автоматично</remarks>
    public abstract class LpBaseRequest
    {       
        /// <summary>
        /// API version
        /// </summary>
        public double Version { get; set; }
        /// <summary>
        /// The unique identifier of your company in the LiqPay system. Filled in automatically.
        /// </summary>
        /// <remarks>Унікальний ідентифікатор Вашої компанії в системі LiqPay. Заповнюється автоматично</remarks>
        public string PublicKey { get; set; } = string.Empty;

        /// <summary>
        /// The URL to which the buyer will be redirected after completing the purchase.
        /// If the parameter is not passed, then the settings in your shop's LiqPay cabinet, API tab are used.
        /// The maximum length is 510 characters.
        /// </summary>
        /// <remarks>URL у Вашому магазині на який покупець буде переадресовано після завершення покупки.
        /// Максимальна довжина 510 символів.</remarks>
        public string? ResultUrl { get; set; }

        /// <summary>
        /// The URL in your store for notifications about changes in payment status (Server-Server model).
        /// If the parameter is not passed, then the settings in your shop's LiqPay cabinet, API tab are used.
        /// The maximum length is 510 characters.
        /// </summary>
        /// <remarks>URL API в Вашому магазині для повідомлень про зміну статусу платежу (сервер->сервер).
        /// Максимальна довжина 510 символів.</remarks>
        public string? ServerUrl { get; set; }


        /// <summary>
        /// List of problematic parameters as a result of Validate
        /// </summary>
        /// <remarks>Список проблемних параметрів у результаті перевірки</remarks>
        [JsonIgnore]
        protected IList<string> ProblemParameterNameList { get; } = new List<string>();

        /// <summary>
        /// Overridden parameter validation. Problem parameter names are placed in the ProblemParameterNameList
        /// </summary>
        /// <remarks>Перевизначена перевірка параметрів. 
        /// Імена проблемних параметрів містяться у списку ProblemParameterNameList</remarks>
        protected virtual void InternalValidate()
        {
            if (string.IsNullOrWhiteSpace(PublicKey))
                ProblemParameterNameList.Add(nameof(PublicKey));
        }

        /// <summary>
        /// Checking parameters. Called automatically before sending.
        /// </summary>
        /// <exception cref="LiqPayRequestException"></exception>
        /// <remarks>Перевірка параметрів. Викликається автоматично перед тим, як надіслати.</remarks>
        public void Validate()
        {
            var list = new List<string>();
            if (ProblemParameterNameList.Count > 0)
                throw new LiqPayRequestException(ProblemParameterNameList.ToArray());
        }
    }
}
