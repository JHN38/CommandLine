using CommandLine.Interfaces;
using CommandLine.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CommandLine;

public static class CommandLineExtensions
{
    public static IHostBuilder UseCommandLine(this IHostBuilder builder)
    {
        return builder.ConfigureServices((app, services) =>
        {
            services.AddSingleton<ICommands, Commands>()
                .AddHostedService<CommandHost>()
                ;
        });
    }
}
