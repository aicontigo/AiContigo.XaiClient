using AiContigo.XaiClient.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace AiContigo.XaiClient.TestsConsole;

class Program
{
    static async Task Main(string[] args)
    {
        // Determine the environment
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        // Load configuration
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        // Bind configuration to XaiClientOptions
        var options = new Configuration.XaiClientOptions();
        configuration.GetSection("Xai").Bind(options);

        // Validate the options
        ILogger<Program> logger = null; // Will be initialized after DI setup
        try
        {
            options.Validate();
        }
        catch (InvalidOperationException ex)
        {
            // Log error and exit
            logger = logger ?? GetLogger(); // Initialize logger if not yet set
            logger.LogError(ex, "Configuration error: {Message}", ex.Message);
            return;
        }

        // Setup DI
        var services = new ServiceCollection();
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog(Log.Logger, dispose: true);
        });

        services.AddXaiClient(opt =>
        {
            opt.ApiKey = options.ApiKey;
            opt.BaseUrl = options.BaseUrl;
            opt.DefaultModel = options.DefaultModel;
        });

        var provider = services.BuildServiceProvider();
        logger = logger ?? provider.GetRequiredService<ILogger<Program>>(); // Set logger after DI

        // Use the client
        var client = provider.GetRequiredService<XaiApiClient>();
        try
        {
            var languageModelsResponse = await client.ListLanguageModels()
                .ExecuteAsync();

            logger.LogInformation("Available models: {Models}", string.Join(", ", languageModelsResponse.Models.Select(m => m.Id)));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during execution: {Message}", ex.Message);
        }
    }

    // Helper method to get logger in case of early failure
    private static ILogger<Program> GetLogger()
    {
        var loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog(Log.Logger, dispose: true));
        return loggerFactory.CreateLogger<Program>();
    }
}