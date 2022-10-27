using Microsoft.Extensions.Configuration;
using SKit.LiqPay.SDK;

namespace ConsoleApp;

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

