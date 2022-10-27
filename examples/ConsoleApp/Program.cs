using ConsoleApp;
using SKit.LiqPay.SDK;
using System.Text.Json;

// Get LiqPay services
var lpCheckoutButtonService = LiqPayClient.Instance.GetCheckoutButtonService();
var lpApiService = LiqPayClient.Instance.GetService();
var lpGateway = lpApiService.ClientGateway;

// New order id
var currentOrderId = Guid.NewGuid().ToString();
var rnd = new Random(DateTime.Now.Millisecond);

// Get form button
Console.Write("Get form button ... ");
var checkoutRequest = new LpCheckoutRequest
{
    Action = LpCheckoutActionPayment.Pay,
    Amount = rnd.Next(1, 20),
    Currency = LpCheckoutCurrency.UAH,
    Description = $"Product {rnd.Next(1000, 9999)}",
    OrderId = currentOrderId,
    Language = "uk"
};
var jsonRequest = JsonSerializer.Serialize(checkoutRequest, LiqPayGateway.JsonSerializerOptions);
var html = lpCheckoutButtonService.GenerateCheckoutButtonFormHtml(checkoutRequest);
SaveToFile(jsonRequest, "CheckoutButtonForm.Request.json");
SaveToFile(html, "CheckoutButtonForm.html");
Console.WriteLine("Ok");

// Get hyperlink
Console.Write("Get hyperlink ... ");
html = lpCheckoutButtonService.GenerateCheckoutHyperlinkHtml(checkoutRequest);
SaveToFile(html, "CheckoutHyperlink.html");
Console.WriteLine("Ok");

// Server-Server checkout request
Console.Write("Server-Server checkout ... ");
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
var content = await lpApiService.CheckoutApiAsync(checkoutApiRequest);
SaveToFile(lpGateway.LastJsonRequest, "CheckoutApi.Request.json");
var (ok, lpPaymentState) = lpApiService.Parse<LpPaymentStateBase>(content!);
SaveToFile(content!, $"CheckoutApi.Response.{OkError(ok)}.json");
Console.WriteLine($"{OkError(ok)}");

// Get payment status
Console.Write("Get Payment State ... ");
content = await lpApiService.GetPaymentStateAsync(currentOrderId);
SaveToFile(lpGateway.LastJsonRequest, "GetPaymentState.Request.json");
var (ok2, lpPaymentState2) = lpApiService.Parse<LpPaymentStateBase>(content!);
SaveToFile(content!, $"GetPaymentState.Response.{OkError(ok2)}.json");
Console.WriteLine($"{OkError(ok)}");

#region Utils
void SaveToFile(string text, string filename)
{
    if (text == null)
        throw new ArgumentNullException(nameof(text));
    if (filename == null)
        throw new ArgumentNullException(nameof(filename));

    var folder = "../../../../../assets/outputs/ConsoleApp";
    if (!Directory.Exists(folder))
        Directory.CreateDirectory(folder);
    
    var path = Path.Combine(folder, filename);
    File.WriteAllText(path, text);
}

string OkError(bool ok) => ok ? "Ok" : "Error";
#endregion

public partial class Program { }
