using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Xunit.Extensions.Ordering;
////Optional
//[assembly: CollectionBehavior(DisableTestParallelization = true)]
//Optional
[assembly: TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
//Optional
[assembly: TestCollectionOrderer("Xunit.Extensions.Ordering.CollectionOrderer", "Xunit.Extensions.Ordering")]

namespace SKit.LiqPay.SDK.Tests
{
    public class InvoiceTests
    {
        private readonly string _customerEmail = "";

        private readonly ILiqPayService _lpService;

        public InvoiceTests(IConfiguration configuration, ILiqPayService lpService)
        {
            _customerEmail = configuration["CustomerEmail"];
            _lpService = lpService;
        }

        [Fact, Order(1)]
        public async Task InvoiceIssuing()
        {
            var request = new LpInvoiceIssuingRequest
            {
                OrderId = Utils.OrderId,
                ExpiredDate = DateTime.UtcNow.AddDays(1),
                Amount = 22,
                Currency = LpCheckoutCurrency.UAH,
                Email = _customerEmail,
                Goods = new List<InvoiceGood>
                {
                    new InvoiceGood
                    {
                        Name = "USB",
                        Count = 2,
                        Amount = 11,
                        Unit = "шт."
                    }
                }
            };
            var lpInvoiceIssuingResponse = await _lpService.InvoiceIssuingAsync(request);
            Utils.SaveToFile(_lpService.ClientGateway.LastJsonRequest,
                $"InvoiceIssuing.Request.json");
            Utils.SaveToFile(_lpService.ClientGateway.LastJsonResponse, "InvoiceIssuing.Response.json");
        }

        [Fact, Order(2)]
        public async Task InvoiceCancel()
        {
            var lpInvoiceCancelResponse = await _lpService.InvoiceCancelAsync(Utils.OrderId);
            Utils.SaveToFile(_lpService.ClientGateway.LastJsonRequest,
                $"InvoiceCancel.Request.json");
            Utils.SaveToFile(_lpService.ClientGateway.LastJsonResponse, "InvoiceCancel.Response.json");
        }
    }
}
