using CommandLine.Interfaces;

namespace CommandLine.Services;

public partial class Commands : List<ICommand>, ICommands
{
    public void PrintHelp()
    {
        Console.WriteLine("Available commands:\r\n");
        foreach (var cmd in this)
        {
            Console.WriteLine("\t{0}\t{1}", cmd.Cmd, cmd.Description);
        }
    }

    public void PrintHelp(string[] parameters)
    {
        if (parameters.Length == 0)
        {
            PrintHelp();
            return;
        }

        if (this.FirstOrDefault(c => c.Cmd?.Equals(parameters[0], StringComparison.OrdinalIgnoreCase) == true) is not ICommand cmd)
        {
            Console.WriteLine(@"Command ""{0}"" not found.", parameters[0]);
            return;
        }

        Console.WriteLine("Command {0}:\r\n", cmd.Cmd);
        Console.WriteLine("{0}", cmd.Description);
    }
}