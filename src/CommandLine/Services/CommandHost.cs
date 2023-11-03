using System.Text.RegularExpressions;
using CommandLine.Interfaces;
using CommandLine.Models;
using Microsoft.Extensions.Hosting;

namespace CommandLine.Services;

public class CommandHost : BackgroundService
{
    private readonly ICommands _commands;

    public CommandHost(ICommands commands)
    {
        _commands = commands;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        await base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(async () =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.Write("> ");
                if (Console.ReadLine() is not string commandLine || string.IsNullOrWhiteSpace(commandLine))
                {
                    continue;
                }

                var args = Regex.Matches(commandLine, @"[\""].+?[\""]|[^ ]+", RegexOptions.IgnoreCase, TimeSpan.FromSeconds(15))
                    .Cast<Match>()
                    .Select(x => x.Value.Trim('"'))
                    .ToArray();

                var commandName = args[0];
                var commandParameters = args[1..];

                ICommand? cmd;

                try
                {
                    cmd = _commands.First(c => c.Cmd.Equals(commandName));
                }
                catch
                {
                    Console.WriteLine(Text.CommandNotFound);
                    continue;
                }

                if (cmd.CmdAction is not null)
                {
                    cmd.CmdAction();
                }
                else if (cmd.CmdStrAction is not null)
                {
                    cmd.CmdStrAction(commandParameters);
                }
                else if (cmd.AsyncCmdAction is not null)
                {
                    await cmd.AsyncCmdAction(commandParameters);
                }
            }
        }, stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
    }
}
