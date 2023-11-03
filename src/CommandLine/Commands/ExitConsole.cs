using CommandLine.Interfaces;

namespace CommandLine.Services;

public partial class Commands : List<ICommand>, ICommands
{
    public static void ExitConsole(string[] parameters)
    {
        Environment.Exit(0);
    }
}
