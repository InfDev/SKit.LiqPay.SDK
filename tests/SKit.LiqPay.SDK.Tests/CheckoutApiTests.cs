using FluentAssertions;
using System.Text.Json;

namespace SKit.LiqPay.SDK.Tests
{
    public class CheckoutApiTests
    {
        private readonly ILiqPayService _lpService;

        public CheckoutApiTests(ILiqPayService lpService)
        {
            _lpService = lpService;
        }

        [Fact]
        public async Task CheckoutApiTest()
        {
            var currentOrderId = Guid.NewGuid().ToString();
            var rnd = new Random(DateTime.Now.Millisecond);

            // Pay
            var checkoutApiRequest = new LpCheckoutApiRequest
            {
                Action = LpCheckoutActionPayment.Pay,
                Amount = rnd.Next(1, 20),
                Currency = LpCheckoutCurrency.UAH,
                Description = $"Product {rnd.Next(1000, 9999)}",
                OrderId = currentOrderId,
                Language = "uk",

                Card = "4242424242424242",
                CardExpMonth = "12",
                CardExpYear = "32",
                CardCvv = "123"
            };
            var content = await _lpService.CheckoutApiAsync(checkoutApiRequest);
            Utils.SaveToFile(_lpService.ClientGateway.LastJsonRequest,
                $"CheckoutApi.Request.json");
            (bool ok, LpPaymentStateBase? lpPaymentState) = _lpService.Parse<LpPaymentStateBase>(content!);
            Utils.SaveToFile(content!, $"CheckoutApi.Response.json");

            ok.Should().BeTrue();
            lpPaymentState!.OrderId.Should().Be(currentOrderId);

            // Get payment state
            content = await _lpService.GetPaymentStateAsync(currentOrderId);
            Utils.SaveToFile(_lpService.ClientGateway.LastJsonRequest,
                "GetPaymentState.Request.json");
            Utils.SaveToFile(content!, $"GetPaymentState.Response.json");
            var (ok2, lpPaymentState2) = _lpService.Parse<LpPaymentStateBase>(content!);
            ok.Should().BeTrue();
            lpPaymentState!.OrderId.Should().Be(currentOrderId);
            lpPaymentState!.Status.Should().Be(LpPaymentStatus.Success);
        }
    }
}
