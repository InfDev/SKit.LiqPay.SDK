using SKit.LiqPay.SDK;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LiqPayServiceCollectionExtensions
    {
        public static IServiceCollection AddLiqPay(this IServiceCollection services,
                        IConfiguration configuration)
        {
            // Registering the gateway options
            var liqPayGatewayOptions = new LiqPayGatewayOptions();
            configuration.GetSection(LiqPayGatewayOptions.LiqPayGatewaySection)
                .Bind(liqPayGatewayOptions);
            services.AddSingleton<LiqPayGatewayOptions>(liqPayGatewayOptions);

            // Registering the gateway base without HttpClient
            services.AddTransient<ILiqPayGatewayBase, LiqPayGatewayBase>();
            // Register custom gateway factory to 'LiqPay' with HttpClient
            services.AddHttpClient<ILiqPayGateway, LiqPayGateway>()
                        // DO NOT USE
                        //.ConfigureHttpClient((serviceProvider, httpClient) =>
                        //{
                        //    var gatewayOptions = serviceProvider.GetRequiredService<LiqPayGatewayOptions>();
                        //    httpClient.BaseAddress = new Uri(gatewayOptions.BaseApiUrl);
                        //    // Set timeout
                        //    var seconds = gatewayOptions.TimeoutSec ?? 0;
                        //    if (seconds <= 0)
                        //        seconds = 100;
                        //    else if (seconds < 60)
                        //        seconds = 60;
                        //    httpClient.Timeout = TimeSpan.FromSeconds(seconds);
                        //    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                        //})
                        //.SetHandlerLifetime(TimeSpan.FromMinutes(2))    // Default is 2 mins
                        //.ConfigurePrimaryHttpMessageHandler(x =>
                        //    new HttpClientHandler
                        //    {
                        //        AutomaticDecompression = DecompressionMethods.All, // DecompressionMethods.GZip | DecompressionMethods.Deflate,
                        //        UseCookies = false,
                        //        AllowAutoRedirect = false,
                        //        UseDefaultCredentials = false,
                        //    })
                        ;

            // Registration of the service for providing Checkout buttons
            // for redirection to a personalized LiqPay payment page
            services.AddTransient<ILiqPayCheckoutButtonService, LiqPayCheckoutButtonService>();
            // Registration of service for working with checkout methods of 'LiqPay' API
            services.AddTransient<ILiqPayService, LiqPayService>();
            // Registration PDT Notification Listener from LiqPay 
            services.AddTransient<ILiqPayPdtListener, LiqPayPdtListener>();
            // Registration IPN Notification Listener from LiqPay
            services.AddTransient<ILiqPayIpnListener, LiqPayIpnListener>();
            return services;
        }
    }
}
