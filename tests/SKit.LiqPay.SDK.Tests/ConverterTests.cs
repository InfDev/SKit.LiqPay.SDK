using FluentAssertions;
using SKit.LiqPay.SDK.Converters;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace SKit.LiqPay.SDK.Tests
{
    // How to Deserialize JSON Into Dynamic Object in C#
    // https://code-maze.com/csharp-deserialize-json-into-dynamic-object/


    public class ConverterTests
    {
        private class TestResponseType
        {
            public LpActionPayment Action { get; set; } = LpActionPayment.Pay;
            public double Amount { get; set; }
            public LpCheckoutCurrency? Currency { get; set; }
            public string? InvoiceId { get; set; }
            public DateTime? CreateDate { get; set; }

        }
                                          
        private const long TestEposhTimeMs = 1661990400000; // GMT: Thursday, 1 September 2022 
        private static DateTime TestDateTime = new DateTime(2022, 9, 1, 0, 0, 0, DateTimeKind.Utc);
        private JsonSerializerOptions _jsonOptions = LiqPayGatewayBase.JsonSerializerOptions;

        private const string TestResponseJson =
            "{\"action\":\"pay\",\"amount\":105.5,\"currency\":\"UAH\",\"invoice_id\":\"\",\"create_date\":\"2022-09-01 00:00:00\"}";
        private static TestResponseType testResponseObj = new TestResponseType
        {
            Action = LpActionPayment.Pay,
            Amount = 105.50,
            Currency = LpCheckoutCurrency.UAH,
            InvoiceId = "",
            CreateDate = TestDateTime,
        };
        
        public ConverterTests()
        {
        }

        [Fact]
        public void CanDeserializeJson()
        {
            var obj = JsonSerializer.Deserialize<TestResponseType>(TestResponseJson, _jsonOptions);
            obj.Should().BeEquivalentTo(testResponseObj);
        }

        [Fact]
        public void CanSerializeJson()
        {
            var json = JsonSerializer.Serialize<TestResponseType>(testResponseObj, _jsonOptions);
            json.Should().Be(TestResponseJson);
        }

        [Fact]
        public void CanEnumerationJson()
        {
            var expected = LpActionPayment.Pay;
            var json = JsonSerializer.Serialize(expected, _jsonOptions);
            var restoreObj = JsonSerializer.Deserialize<LpActionPayment>(json, _jsonOptions);
            restoreObj
                .Should().Be(expected);
        }
    }
}