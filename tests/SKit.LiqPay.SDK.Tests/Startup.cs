
namespace SKit.LiqPay.SDK.Tests
{
    public class Startup
    {
        public void ConfigureHost(IHostBuilder hostBuilder) =>
            hostBuilder
                .ConfigureHostConfiguration(builder => {
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                    builder.AddJsonFile("appsettings.json", optional: false);
                    builder.AddUserSecrets<Startup>();
                })
                .ConfigureAppConfiguration((context, builder) => {
                });

        public void ConfigureServices(IServiceCollection services, 
                        HostBuilderContext context)
        {
            services.AddLiqPay(context.Configuration);
        }
    }
}
