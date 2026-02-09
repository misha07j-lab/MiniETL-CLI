using MiniEtl.Cli.Help;
using MiniEtl.Core.Factories;

using System.Reflection;
using System.Text;
namespace MiniEtl.Cli;

class Program
{
    static async Task<int> Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        var version = Assembly.GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion
            ?? "unknown";

        try
        {
            if (args.Contains("--help"))
            {
                HelpPrinter.Print(MiniEtlCliDescriptor.Build(version));
                return ExitCodes.Success;
            }

            if (args.Contains("--version"))
            {
                Console.WriteLine($"mini-etl v{version}");
                return ExitCodes.Success;
            }

            if (args.Length == 0)
            {
                HelpPrinter.Print(MiniEtlCliDescriptor.Build(version));
                return ExitCodes.InvalidArguments;
            }
            var options = ArgumentParser.Parse(args);
            if (options == null)
                return ExitCodes.InvalidArguments;

            var processor = ProcessorFactory.Create(options);

            var result = await processor.ProcessAsync(options);

            var message = ErrorMessageMapper.GetMessage(result);
            if (message is not null)
            {
                Console.Error.WriteLine($"Error: {message}");
            }

            return ExitCodeMapper.Map(result);

        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unhandled error: {ex.Message}");
            return ExitCodes.UnhandledException;
        }

    }
}



