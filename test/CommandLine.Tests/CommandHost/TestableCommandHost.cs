using System.Threading;
using System.Threading.Tasks;
using CommandLine.Interfaces;
using CommandLine.Services;

namespace CommandLine.Tests;

public class TestableCommandHost(ICommands commands) : CommandHost(commands)
{
    public new Task ExecuteAsync(CancellationToken stoppingToken)
        => base.ExecuteAsync(stoppingToken);
}
