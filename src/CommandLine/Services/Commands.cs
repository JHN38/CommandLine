using CommandLine.Interfaces;
using CommandLine.Models;

namespace CommandLine.Services;

public partial class Commands : List<ICommand>, ICommands
{
    public Commands()
    {
        Add("h", cmdStrAction: PrintHelp, "Lists available commands and get help on a command <cmd>");
        Add("e", Commands.ExitConsole, "Exits the console");
    }

    public ICommands Add(string cmd, Action cmdAction, string? description = null)
    {
        Add(new Command(cmd, cmdAction, description));

        return this;
    }

    public ICommands Add(string cmd, Action<string[]> cmdStrAction, string? description = null)
    {
        Add(new Command(cmd, cmdStrAction, description));

        return this;
    }

    public ICommands Add(string cmd, Func<string[], Task> cmdActionAsync, string? description = null)
    {
        Add(new Command(cmd, cmdActionAsync, description));

        return this;
    }

    public new void Add(ICommand item)
    {
        if (Exists(c => c.Cmd == item.Cmd))
        {
            throw new ArgumentException(@$"A command ""{item.Cmd}"" already exists.");
        }

        base.Add(item);
    }
}
