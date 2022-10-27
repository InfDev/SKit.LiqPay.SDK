using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SKit.LiqPay.SDK.Tests
{
    public class PlaygroundApplication : WebApplicationFactory<Program>
    {
        private readonly string _environment;

        public PlaygroundApplication(string environment = "Development") : base()
        {
            _environment = environment;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(_environment);

            // Add mock/test services to the builder here
            builder.ConfigureServices(services =>
            {
                services.AddScoped(sp =>
                {
                    //// Replace SQL Lite with test DB in memory
                    //return new SqliteConnection("Data Source=:memory:");
                    // Replace SQL Lite with test DB
                    return new SqliteConnection("Data Source=testapp.db");
                });
            });

            return base.CreateHost(builder);
        }
    }
}
