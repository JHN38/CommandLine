namespace CommandLine.Interfaces;

public interface ICommand
{
    string Cmd { get; set; }
    Action? CmdAction { get; set; }
    Action<string[]>? CmdStrAction { get; set; }
    Func<string[], Task>? AsyncCmdAction { get; set; }
    string? Description { get; set; }
}
