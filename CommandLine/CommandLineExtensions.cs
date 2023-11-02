using CommandLine.Interfaces;
using CommandLine.Models;
using CommandLine.Services;

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
