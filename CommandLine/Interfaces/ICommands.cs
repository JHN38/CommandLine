namespace CommandLine.Interfaces;

public interface ICommands : IList<ICommand>
{
    ICommands Add(string cmd, Action cmdAction, string? description = null);
    ICommands Add(string cmd, Action<string[]> cmdStrAction, string? description = null);
    ICommands Add(string cmd, Func<string[], Task> cmdActionAsync, string? description = null);

    void PrintHelp();
    void PrintHelp(string[] parameters);
}
