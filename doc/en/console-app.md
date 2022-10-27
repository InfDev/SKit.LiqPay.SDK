# Console App & SKit.LiqPaySDK [⇑](index.md)

## General information

Using the SDK.
An example of a console application without receiving a PDT notification.

Functionality:

- formation of payment buttons
- payment through personal page LiqPay
- card payment via API
- getting payment state
- logging of exchange with LiqPay

Before running the demo application:

- create an account in LiqPay, copy your keys and add them to appsettings.json.

## Output

Console

``` txt
Get form button ... Ok
Get hyperlink ... Ok
Server-Server checkout ... Ok
Get Payment State ... Ok
```

The output files of the operations for generating a payment button and exchanging with LiqPay are placed in the solution folder **assets/outputs/ConsoleApp**.

## LiqPay client

The application does not use dependency injection, and the LiqPay client is designed as a Singleton class. HttpClient is activated only for the lifetime of the LiqPayService service object and no more, because sockets are not a limitless resource.

``` csharp
public sealed class LiqPayClient
{
    private LiqPayGatewayOptions _gatewayOptions;

    private static readonly Lazy<LiqPayClient> LazyInstance = new Lazy<LiqPayClient>(() => new LiqPayClient());
    private LiqPayClient()
    {
        _gatewayOptions = AppConfig.Instance.
            Configuration.GetSection("LiqPayGateway").Get<LiqPayGatewayOptions>();
    }

    public static LiqPayClient Instance => LazyInstance.Value;

    public LiqPayGatewayOptions GatewayOptions => _gatewayOptions;

    public LiqPayCheckoutButtonService GetCheckoutButtonService() =>
        new LiqPayCheckoutButtonService(new LiqPayGatewayBase(GatewayOptions));

    public LiqPayService GetService() =>
        new LiqPayService(new LiqPayGateway(new HttpClient(), GatewayOptions));
}
```
