using CommandLine.Interfaces;

namespace CommandLine.Models;

public class Command : ICommand
{
    public string? Description { get; set; }
    public string Cmd { get; set; }
    public Action? CmdAction { get; set; }
    public Action<string[]>? CmdStrAction { get; set; }
    public Func<string[], Task>? AsyncCmdAction { get; set; }

    public Command(string cmd, Action cmdAction, string? description = null)
    {
        Cmd = cmd;
        CmdAction = cmdAction;
        Description = description;
    }

    public Command(string cmd, Action<string[]> cmdStrAction, string? description = null)
    {
        Cmd = cmd;
        CmdStrAction = cmdStrAction;
        Description = description;
    }

    public Command(string cmd, Func<string[], Task> asyncCmdAction, string? description = null)
    {
        Cmd = cmd;
        AsyncCmdAction = asyncCmdAction;
        Description = description;
    }
}
