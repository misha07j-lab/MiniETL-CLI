namespace MiniEtl.Cli;

public static class ExitCodes
{
    public const int Success = 0;

    // CLI errors (1–10)
    public const int InvalidArguments = 1;

    // Processing errors (10–50)
    public const int FileNotFound = 10;
    public const int ProcessingError = 20;

    // Fatal
    public const int UnhandledException = 99;
}


