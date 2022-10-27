
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Basic service that allows you to interact with LiqPay through a personalized payment page.
    /// See <see href="https://www.liqpay.ua/en/documentation/api/aquiring/checkout">ENG</see>
    /// or <see href="https://www.liqpay.ua/documentation/api/aquiring/checkout">UKR</see> dоcumentation.
    /// </summary>
    public interface ILiqPayCheckoutButtonService
    {
        /// <summary>
        /// Replaceable element for Data value
        /// </summary>
        /// <remarks>Підмінний елемент для значення Data</remarks>
        public const string TemplateElementData = "{{DATA}}";
        /// <summary>
        /// Replaceable element for Signature value
        /// </summary>
        /// <remarks>Підмінний елемент для значення Signature</remarks>
        public const string TemplateElementSignature = "{{SIGNATURE}}";
        /// <summary>
        /// Replaceable element for the text of the payment button
        /// </summary>
        /// <remarks>Підмінний елемент для тексту кнопки платежу</remarks>
        public const string TemplateElementButtonText = "{{BUTTON_TEXT}}";

        /// <summary>
        /// Generates an HTML form with a button to redirect to a personalized LiqPay payment page
        /// </summary>
        /// <param name="request">Usually LpCheckoutRequest</param>
        /// <param name="displaySum">Display amount in currency</param>
        /// <param name="customTemplate">Custom template, see <see cref="DefaultCheckoutButtonFormTemplate"/></param>
        /// <returns></returns>
        /// <remarks>Створює HTML-форму з кнопкою для перенаправлення на персоналізовану платіжну сторінку LiqPay</remarks>
        string GenerateCheckoutButtonFormHtml(LpCheckoutRequest request, bool displaySum = true, string? customTemplate = null);

        /// <summary>
        /// Generates an HTML hyperlink as button to redirect to a personalized LiqPay payment page
        /// </summary>
        /// <param name="request">Usually LpCheckoutRequest</param>
        /// <param name="displaySum">Display amount in currency</param>
        /// <param name="customTemplate">Custom template, see <see cref="DefaultCheckoutHyperlinkTemplate"/></param>
        /// <returns></returns>
        string GenerateCheckoutHyperlinkHtml(LpCheckoutRequest request, bool displaySum = true, string? customTemplate = null);

        /// <summary>
        /// Default payment html button template.
        /// The template must contain the following elements to be replaced by values:
        /// <list type="bullet">
        /// <item>{{DATA}}</item>
        /// <item>{{SIGNATURE}}</item>
        /// <item>{{BUTTON_TEXT}} - optional.
        /// If present, it is replaced by "Pay 5 USD" (as an example)
        /// in the corresponding language in the request</item>
        /// </list> 
        /// </summary>
        /// <remarks>Шаблон платіжної html-кнопки за замовчуванням</remarks>
        string DefaultCheckoutButtonFormTemplate { get; }

        /// <summary>
        /// Default payment html link as button template.
        /// The template must contain the following elements to be replaced by values:
        /// <list type="bullet">
        /// <item>{{DATA}}</item>
        /// <item>{{SIGNATURE}}</item>
        /// <item>{{BUTTON_TEXT}} - optional.
        /// If present, it is replaced by "Pay 5 USD" (as an example)
        /// in the corresponding language in the request</item>
        /// </list> 
        /// </summary>
        /// <remarks>Шаблон платіжної html-посилання у вигляді кнопки (GET) за замовчуванням</remarks>
        string DefaultCheckoutHyperlinkTemplate { get; }
    }
}
