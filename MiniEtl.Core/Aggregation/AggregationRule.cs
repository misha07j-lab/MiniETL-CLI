namespace MiniEtl.Core.Aggregation;

public class AggregationRule
{
    public string Column { get; set; } = string.Empty;
    public string Function { get; set; } = string.Empty; // count, sum, avg
}

