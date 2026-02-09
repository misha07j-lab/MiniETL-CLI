using MiniEtl.Core.Contracts;

namespace MiniEtl.Cli;

public static class ArgumentParser
{
    public static ProcessingOptions? Parse(string[] args)
    {
        var options = new ProcessingOptions();

        string? input = null;
        string? output = null;

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i].ToLower())
            {
                case "--input":
                    if (i + 1 < args.Length)
                        input = args[++i];
                    break;

                case "--output":
                    if (i + 1 < args.Length)
                        output = args[++i];
                    break;

                case "--filter":
                    if (i + 1 < args.Length)
                        options.FilterExpression = args[++i];
                    break;

                case "--verbose":
                    options.Verbose = true;
                    break;

                case "--quiet":
                    options.Quiet = true;
                    break;
            }
        }

        if (string.IsNullOrWhiteSpace(input) ||
            string.IsNullOrWhiteSpace(output))
            return null;

        options.InputPath = input;
        options.OutputPath = output;

        return options;
    }
}

