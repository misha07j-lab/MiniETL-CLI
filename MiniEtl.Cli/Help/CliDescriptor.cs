namespace MiniEtl.Cli.Help;

public class CliDescriptor
{
    public string ToolName { get; init; } = "";
    public string Description { get; init; } = "";
    public string Version { get; init; } = "";

    public List<CliOption> Required { get; init; } = new();
    public List<CliOption> Optional { get; init; } = new();
}

