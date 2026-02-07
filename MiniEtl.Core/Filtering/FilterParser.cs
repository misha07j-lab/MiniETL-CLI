namespace MiniEtl.Core.Filtering;

public sealed class FilterParseException : Exception
{
    public int Position { get; }

    public FilterParseException(string message, int position)
        : base($"{message} (pos {position})")
    {
        Position = position;
    }
}

public static class FilterParser
{
    // Example: "Status=Open;Amount>=100;Name~john"


    private static (string field, FilterOperator op, string value) ParseSingle(string s, int basePos)
    {
        // operator priority: 2-char first
        var ops2 = new[] { ">=", "<=", "!=" };
        foreach (var op2 in ops2)
        {
            var idx = s.IndexOf(op2, StringComparison.Ordinal);
            if (idx > -1)
                return Build(s, idx, op2.Length, Map(op2), basePos);
        }

        // then 1-char
        var ops1 = new[] { "=", ">", "<", "~", "^", "$" };
        foreach (var op1 in ops1)
        {
            var idx = s.IndexOf(op1, StringComparison.Ordinal);
            if (idx > -1)
                return Build(s, idx, op1.Length, Map(op1), basePos);
        }

        throw new FilterParseException(
            "Missing operator. Use =, !=, >, >=, <, <=, ~ (contains), ^ (startsWith), $ (endsWith)",
            basePos);
    }

    private static (string field, FilterOperator op, string value) Build(
        string s, int opIndex, int opLen, FilterOperator op, int basePos)
    {
        var field = s.Substring(0, opIndex).Trim();
        var value = s.Substring(opIndex + opLen).Trim();

        if (field.Length == 0)
            throw new FilterParseException("Field name is empty", basePos + opIndex);

        if (value.Length == 0)
            throw new FilterParseException("Value is empty", basePos + opIndex + opLen);

        return (field, op, value);
    }

    private static FilterOperator Map(string op) => op switch
    {
        "=" => FilterOperator.Eq,
        "!=" => FilterOperator.Neq,
        ">" => FilterOperator.Gt,
        ">=" => FilterOperator.Gte,
        "<" => FilterOperator.Lt,
        "<=" => FilterOperator.Lte,
        "~" => FilterOperator.Contains,
        "^" => FilterOperator.StartsWith,
        "$" => FilterOperator.EndsWith,
        _ => throw new ArgumentOutOfRangeException(nameof(op), op, "Unsupported operator")
    };
}


