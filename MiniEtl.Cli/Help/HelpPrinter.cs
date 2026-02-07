using System;
using System.Linq;

namespace MiniEtl.Cli.Help;

public static class HelpPrinter
{
    public static void Print(CliDescriptor descriptor)
    {
        Console.WriteLine($@"
{descriptor.ToolName} v{descriptor.Version}
{descriptor.Description}

Usage:
  {descriptor.ToolName} --input <file> --output <file> [options]
");

        if (descriptor.Required.Any())
        {
            Console.WriteLine("Required:");
            foreach (var opt in descriptor.Required)
                Console.WriteLine($"  {opt.Name,-20} {opt.Description}");
        }

        if (descriptor.Optional.Any())
        {
            Console.WriteLine("\nOptional:");
            foreach (var opt in descriptor.Optional)
                Console.WriteLine($"  {opt.Name,-20} {opt.Description}");
        }

        Console.WriteLine(@"
Exit Codes:
  0   Success
  1   Invalid arguments
  10  File not found
  20  Processing error
  99  Unhandled exception
");
    }
}
