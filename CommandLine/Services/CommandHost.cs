using System.Text.RegularExpressions;
using CommandLine.Interfaces;
using CommandLine.Models;

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
               if (Console.ReadLine() is string commandLine && !string.IsNullOrWhiteSpace(commandLine))
               {
                   var args = Regex.Matches(commandLine, @"[\""].+?[\""]|[^ ]+")
                       .Cast<Match>()
                       .Select(x => x.Value.Trim('"'))
                       .ToArray();

                   var commandName = args[0];
                   var commandParameters = args[1..];

                   if (_commands.FirstOrDefault(c => c.Cmd.Equals(commandName)) is Command cmd)
                   {
                       if (cmd.CmdAction is not null)
                           cmd.CmdAction();

                       if (cmd.CmdStrAction is not null)
                           cmd.CmdStrAction(commandParameters);

                       if (cmd.AsyncCmdAction is not null)
                           await cmd.AsyncCmdAction(commandParameters);
                   }
                   else
                   {
                       Console.WriteLine("Command not found.");
                   }
               }
           }
       }, stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
    }
}
