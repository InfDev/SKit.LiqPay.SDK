using FluentAssertions;
using System.Text.Json;

namespace SKit.LiqPay.SDK.Tests
{
    public class CheckoutButtonTests
    {
        private readonly ILiqPayCheckoutButtonService _lpCheckoutButtonService;
        private readonly ILiqPayPdtListener _lpListener;

        public CheckoutButtonTests(ILiqPayCheckoutButtonService lpCheckoutButtonService,
            ILiqPayPdtListener lpListener)
        {
            _lpCheckoutButtonService = lpCheckoutButtonService;
            _lpListener = lpListener;
        }

        [Fact]
        public void FormButtonTest()
        {
            double sum = 16;
            var sumStr = sum.ToString();
            var orderId = "1578";
            var checkoutRequest = new LpCheckoutRequest
            {
                Action = LpCheckoutActionPayment.Pay,
                Amount = sum,
                Currency = LpCheckoutCurrency.UAH,
                Description = "USB cabel",
                OrderId = orderId,
                Language = "uk"
            };

            checkoutRequest.Language = "uk";
            checkoutRequest.Currency = LpCheckoutCurrency.UAH;
            var jsonRequest = JsonSerializer.Serialize(checkoutRequest, LiqPayGateway.JsonSerializerOptions);
            var html = _lpCheckoutButtonService.GenerateCheckoutButtonFormHtml(checkoutRequest, true);
            Utils.SaveToFile(jsonRequest, $"CheckoutButtonForm.Request.{checkoutRequest.Language}.json");
            Utils.SaveToFile(html, $"CheckoutButtonForm.{checkoutRequest.Language}.html");
            html.Should().Contain($"Сплатити {sumStr} UAH")
                .And.Contain("<form method=\"POST\" accept-charset=\"utf-8\" action=\"https://www.liqpay.ua/api/3/checkout\">")
                .And.Contain("name=\"data\"")
                .And.Contain("name=\"signature\"");

            var signature = ExtractValueFromHtmlForm(html, "signature");
            var data = ExtractValueFromHtmlForm(html, "data");
            (bool ok, LpPaymentStateBase? request) = _lpListener.DecodingParser(data!, signature!);
            ok.Should().BeTrue();
            request!.Amount.Should().Be(sum);
            request!.OrderId.Should().Be(orderId);

            checkoutRequest.Language = "uk";
            checkoutRequest.Currency = LpCheckoutCurrency.USD;
            jsonRequest = JsonSerializer.Serialize(checkoutRequest, LiqPayGateway.JsonSerializerOptions);
            html = _lpCheckoutButtonService.GenerateCheckoutButtonFormHtml(checkoutRequest, true);
            Utils.SaveToFile(jsonRequest, $"CheckoutButtonForm.Request.{checkoutRequest.Language}.json");
            Utils.SaveToFile(html, $"CheckoutButtonForm.{checkoutRequest.Language}.html");
            html.Should().Contain($"Сплатити {sumStr} USD");

            checkoutRequest.Language = "en";
            checkoutRequest.Currency = LpCheckoutCurrency.USD;
            jsonRequest = JsonSerializer.Serialize(checkoutRequest, LiqPayGateway.JsonSerializerOptions);
            html = _lpCheckoutButtonService.GenerateCheckoutButtonFormHtml(checkoutRequest, true);
            Utils.SaveToFile(jsonRequest, $"CheckoutButtonForm.Request.{checkoutRequest.Language}.json");
            Utils.SaveToFile(html, $"CheckoutButtonForm.{checkoutRequest.Language}.html");
            html.Should().Contain($"Pay {sumStr} USD");

            checkoutRequest.Language = "uk";
            checkoutRequest.Currency = LpCheckoutCurrency.EUR;
            jsonRequest = JsonSerializer.Serialize(checkoutRequest, LiqPayGateway.JsonSerializerOptions);
            html = _lpCheckoutButtonService.GenerateCheckoutButtonFormHtml(checkoutRequest, true);
            Utils.SaveToFile(jsonRequest, $"CheckoutButtonForm.Request.{checkoutRequest.Language}.json");
            Utils.SaveToFile(html, $"CheckoutButtonForm.{checkoutRequest.Language}.html");
            html.Should().Contain($"Сплатити {sumStr} EUR");
        }

        private static string? ExtractValueFromHtmlForm(string html, string valueName)
        {
            // signature = "Mpuo/f+LTm/S2iZF6HAVzraJ3iE="
            
            var template = $"name=\"{valueName}\" value=\"";
            var len = template.Length;
            var pos = html.IndexOf(template, StringComparison.Ordinal);
            if (pos == -1)
                return null;
            pos += len;
            var endPos = pos;
            for(var i=pos; i < html.Length; i++, endPos++)
            {
                if (html[i] == '"')
                {
                    return html.Substring(pos, endPos - pos);
                }
            }
            return null;
        }
    }
}
