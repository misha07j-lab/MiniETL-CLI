using MiniEtl.Core.Aggregation;
using MiniEtl.Core.Filtering;
using MiniEtl.Core.Transforming;

namespace MiniEtl.Core.Contracts;

public class ProcessingOptions
{
    public string InputPath { get; set; } = string.Empty;
    public string OutputPath { get; set; } = string.Empty;
    public string? FilterExpression { get; set; }
    public string? TransformExpression { get; set; }
    public string? AggregateExpression { get; set; }
    public List<FilterRule>? FilterRules { get; set; } = new();
    public List<TransformRule>? TransformRules { get; set; } = new();
    public List<AggregationRule> AggregationRules { get; set; } = new();
    public bool Verbose { get; set; }
    public bool Quiet { get; set; }
}

