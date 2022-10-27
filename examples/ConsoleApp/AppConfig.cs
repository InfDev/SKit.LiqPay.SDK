using Microsoft.Extensions.Configuration;

namespace ConsoleApp;

public sealed class AppConfig
{
    private IConfiguration _config;
    private string? _netCoreEnvironment;
    private static readonly Lazy<AppConfig> LazyInstance = new Lazy<AppConfig>(() => new AppConfig());
    private AppConfig()
    {
        _config = Build();
    }

    public static AppConfig Instance => LazyInstance.Value;

    public string? NetCoreEnvironment => _netCoreEnvironment;

    public IConfiguration Configuration => _config;

    private IConfigurationRoot Build()
    {
        _netCoreEnvironment = System.Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{_netCoreEnvironment}.json", optional: true)
            .AddUserSecrets<Program>()
            .AddEnvironmentVariables();
        return builder.Build();
    }
}
