namespace MiniEtl.Core.Filtering;

public enum FilterOperator
{
    Eq, Neq, Gt, Gte, Lt, Lte,
    Contains, StartsWith, EndsWith
}

public sealed record FilterRule(
    string Field,
    FilterOperator Operator,
    string Value);

