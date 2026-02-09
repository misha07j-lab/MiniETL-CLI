using MiniEtl.Core.Contracts;

namespace MiniEtl.Cli;

public static class ErrorMessageMapper
{
    public static string? GetMessage(ProcessingResult result)
    {
        return result switch
        {
            ProcessingResult.FileNotFound =>
                "Input file not found.",

            ProcessingResult.ProcessingError =>
                "Processing error occurred.",

            _ => null
        };
    }
}

