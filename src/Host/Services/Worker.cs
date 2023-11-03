using CommandLine.Interfaces;

namespace Host.Services;

public class Worker : IHostedService
{
    private readonly ICommands _commands;

    public Worker(ICommands commands)
    {
        _commands = commands;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _commands.Add("test", Test, "Test Command");
        _commands.PrintHelp();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public static void Test(string[] parameters)
    {
        Console.WriteLine("This is a test.");
    }
}
