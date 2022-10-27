
namespace SKit.LiqPay.SDK
{
    /// <summary>
    /// Basic service that allows you to interact with LiqPay through a personalized payment page.
    /// See <see href="https://www.liqpay.ua/en/documentation/api/aquiring/checkout">ENG</see>
    /// or <see href="https://www.liqpay.ua/documentation/api/aquiring/checkout">UKR</see> dоcumentation.
    /// </summary>
    public class LiqPayCheckoutButtonService : ILiqPayCheckoutButtonService
    {
        #region Consts
        private const string _DefaultCheckoutButtonFormTemplate =
@"<form method=""POST"" accept-charset=""utf-8"" action=""https://www.liqpay.ua/api/3/checkout"">
  <input type=""hidden"" name=""data"" value=""{{DATA}}"" />
  <input type=""hidden"" name=""signature"" value=""{{SIGNATURE}}"" />
  <button style=""border: none !important; display:inline-block !important;text-align: center !important;padding: 7px 10px !important;
    color: #fff !important; font-size:16px !important; font-weight: 600 !important; font-family:OpenSans, sans-serif; cursor: pointer !important; border-radius: 2px !important;
    background: rgba(122,183,43,1) !important;"" onmouseover=""this.style.opacity='0.5';"" onmouseout=""this.style.opacity='1';"">
    {{BUTTON_TEXT}} &nbsp;&#10148;
  </button>
</form>";

        private const string _DefaultCheckoutHyperlinkTemplate =
@"<a 
href=""https://www.liqpay.ua/api/3/checkout?data={{DATA}}&signature={{SIGNATURE}}""
style=""
border: none !important;
display: inline-block !important;
text-align: center !important;
padding: 7px 10px !important;
color: #fff !important;
font-size: 16px !important;
font-weight: 600 !important;
font-family: OpenSans, sans-serif;
cursor: pointer !important;
border-radius: 2px !important;
background: rgba(122, 183, 43, 1) !important;
text-decoration: none;""
onmouseover=""this.style.opacity='0.5';""
onmouseout=""this.style.opacity='1';""
>
  {{BUTTON_TEXT}} &nbsp;&#10148;
</a>";
        #endregion

        #region Fields
        private readonly ILiqPayGatewayBase _gatewayBase;
        #endregion

        #region Ctor
        public LiqPayCheckoutButtonService(ILiqPayGatewayBase gatewayBase)
        {
            _gatewayBase = gatewayBase;
        }
        #endregion

        #region Implementation ILiqPayCheckoutClientService
        /// <inheritdoc/>
        public string DefaultCheckoutButtonFormTemplate => _DefaultCheckoutButtonFormTemplate;
        /// <inheritdoc/>
        public string DefaultCheckoutHyperlinkTemplate => _DefaultCheckoutHyperlinkTemplate;
        /// <inheritdoc/>
        public string GenerateCheckoutButtonFormHtml(LpCheckoutRequest request, bool displaySum = true, string? customTemplate = null)
        {
            return GenerateHtml(request, displaySum, customTemplate ?? DefaultCheckoutButtonFormTemplate);
        }
        /// <inheritdoc/>
        public string GenerateCheckoutHyperlinkHtml(LpCheckoutRequest request, bool displaySum = true, string? customTemplate = null)
        {
            return GenerateHtml(request, displaySum, customTemplate ?? DefaultCheckoutHyperlinkTemplate);
        }
        #endregion

        #region Utils
        private string TemplatePassThroughValidator(string html)
        {
            if (html == null)
                throw new ArgumentNullException(nameof(html));
            if (!html.Contains(ILiqPayCheckoutButtonService.TemplateElementData) 
                || !html.Contains(ILiqPayCheckoutButtonService.TemplateElementSignature))
                throw new Exception($"The template must contain the following required elements: {ILiqPayCheckoutButtonService.TemplateElementData} and {ILiqPayCheckoutButtonService.TemplateElementSignature}");
            return html;
        }

        private string GetButtonText(LpCheckoutRequest request, bool displaySum = true)
        {
            var btnText = string.Empty;
            var language = request.Language ?? LiqPayConsts.DefaultLanguage;
            var paymentPhrase = string.Empty;
            switch (language)
            {
                case "en": paymentPhrase = "Pay"; break;
                case "ru": paymentPhrase = "Оплатить"; break;
                default: paymentPhrase = "Сплатити"; break;
            }
            btnText = displaySum ? $"{paymentPhrase} {request.Amount} {request.Currency.Name}" : paymentPhrase;
            return btnText;
        }

        private string GenerateHtml(LpCheckoutRequest request, bool displaySum, string template)
        {
            var templ = TemplatePassThroughValidator(template);
            var pack = _gatewayBase.CreatePackage(request)!;
            var btnText = GetButtonText(request, displaySum);

            var html = templ.Replace(ILiqPayCheckoutButtonService.TemplateElementData, pack.Data)
                .Replace(ILiqPayCheckoutButtonService.TemplateElementSignature, pack.Signature)
                .Replace(ILiqPayCheckoutButtonService.TemplateElementButtonText, btnText);
            return html;
        }
        #endregion
    }
}
