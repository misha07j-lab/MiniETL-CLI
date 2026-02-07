using MiniEtl.Core.Contracts;

namespace MiniEtl.Core.Abstractions;

public interface IDataProcessor
{
    Task<ProcessingResult> ProcessAsync(ProcessingOptions options);
}


