namespace MiniEtl.Core.Transforming;

public class TransformRule
{
    public string Column { get; set; } = string.Empty;
    public string Operation { get; set; } = string.Empty; // upper, lower, trim
}

