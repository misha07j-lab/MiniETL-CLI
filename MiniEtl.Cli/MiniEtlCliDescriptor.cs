using MiniEtl.Cli.Help;

namespace MiniEtl.Cli;

public static class MiniEtlCliDescriptor
{
    private const string ToolName = "mini-etl";

    public static CliDescriptor Build(string version) =>
        new()
        {
            ToolName = ToolName,
            Version = version,
            Description = "Studio Standard",
            Required = new()
            {
                new CliOption { Name = "--input <file>", Description = "Input CSV file" },
                new CliOption { Name = "--output <file>", Description = "Output CSV file" }
            },
            Optional = new()
            {
                new CliOption { Name = "--filter \"expr\"", Description = "Filter expression" },
                new CliOption { Name = "--transform \"expr\"", Description = "Transform rules" },
                new CliOption { Name = "--aggregate \"expr\"", Description = "Aggregation rules" },
                new CliOption { Name = "--version", Description = "Show version" },
                new CliOption { Name = "--help", Description = "Show help" }
            }
        };
}
