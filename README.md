# CommandLine

The **CommandLine** package adds a quick and intuitive command line system to an **IHostedService** worker.

#### Initialization

First the **CommandLine** using needs to be added into the Program.cs

```c#
using CommandLine;
```

And then the **UseCommandLine** extension can be added to the **IHostBuilder** to initialize it.

```c#
var builder = Host.CreateDefaultBuilder(args);
builder.UseCommandLine();
```

#### Usage

The **CommandLine** will typically run within a hosted service such as a **IHostedService** or **BackgroundService**.

Add the using to the top of the worker's file.

```c#
using CommandLine.Interfaces;
```

An ICommand can then be injected directly into the worker's constructor.

```c#
    private readonly ICommands _commands;

    public Worker(ICommands commands)
    {
        _commands = commands;
    }
```

The commands would normally be added to the **StartAsync** method, although they can be added or removed at any point.  
In this example we add the "g" command linked to the **Hello** method, and a description hinting at parameters that can be supplied.

```c#
_commands.Add("hello", Hello, "Prints a Hello World! message.");
```

#### Example

Here's a full example:

*Program.cs*

```c#
using CommandLine;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
    .UseContentRoot(Directory.GetCurrentDirectory());

builder.ConfigureServices((host, services) =>
{
    services.AddHostedService<Worker>();
})
.UseCommandLine();

var app = builder.Build();
await app.RunAsync();
```

*Worker.cs*

```c#
using CommandLine.Interfaces;

public class Worker : IHostedService
{
    private readonly ICommands _commands;

    public Worker(ICommands commands)
    {
        _commands = commands;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _commands
            .Add("hello", Hello, "Prints a Hello World! message.");

        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    public async Task Hello(string[] parameters)
    {
    	Console.WriteLine("Hello World!");
        
        await Task.CompletedTask;
    }
}
```
