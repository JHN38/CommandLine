using CommandLine;
using Host.Services;
using Serilog;

var host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((app, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile($"appSettings.json", true, true)
        .AddEnvironmentVariables()
        .AddCommandLine(args);
    })
    .ConfigureServices((host, services) =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog((app, config) => config.ReadFrom.Configuration(app.Configuration))
    .UseCommandLine()
    .Build();

host.Services.GetRequiredService<ILogger<Program>>().LogInformation("Application is starting...");

await host.RunAsync();
