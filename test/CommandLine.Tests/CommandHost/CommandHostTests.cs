using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CommandLine.Interfaces;
using CommandLine.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CommandLine.Tests;

[TestClass]
public class CommandHostTests
{
    private Mock<ICommands>? _commandsMock;
    private TestableCommandHost? _commandHost;

    [TestInitialize]
    public void Setup()
    {
        _commandsMock = new Mock<ICommands>();
        _commandHost = new TestableCommandHost(_commandsMock.Object);
    }

    [TestMethod]
    public async Task ExecuteAsync_CommandNotFound_WritesToConsole()
    {
        // Arrange
        var input = new StringReader("unknown-command\n");
        Console.SetIn(input);

        var output = new StringWriter();
        Console.SetOut(output);

        var cts = new CancellationTokenSource();
        cts.CancelAfter(100);  // Cancel after a short delay to allow the method to enter its loop

        // Act
        await _commandHost!.ExecuteAsync(cts.Token);

        // Assert
        var expectedOutput = Text.CommandNotFound;
        StringAssert.Contains(output.ToString(), expectedOutput);
    }
}