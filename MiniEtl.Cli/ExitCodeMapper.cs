using MiniEtl.Core.Contracts;

namespace MiniEtl.Cli;

public static class ExitCodeMapper
{
    public static int Map(ProcessingResult result)
    {
        return result switch
        {
            ProcessingResult.Success => ExitCodes.Success,
            ProcessingResult.FileNotFound => ExitCodes.FileNotFound,
            ProcessingResult.ProcessingError => ExitCodes.ProcessingError,
            _ => ExitCodes.UnhandledException
        };
    }
}


