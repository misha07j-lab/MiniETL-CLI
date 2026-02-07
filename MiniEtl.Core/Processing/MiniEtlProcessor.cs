using MiniEtl.Core.Abstractions;
using MiniEtl.Core.Aggregation;
using MiniEtl.Core.Contracts;
using MiniEtl.Core.Filtering;
using MiniEtl.Core.IO;
using MiniEtl.Core.Models;
using MiniEtl.Core.Transforming;
using System.Globalization;

namespace MiniEtl.Core.Processing;

public class MiniEtlProcessor : IDataProcessor
{
    public DataTableModel Execute(DataTableModel table, ProcessingOptions options)
    {
        if (options.FilterRules?.Count > 0)
            table = ApplyFilters(table, options.FilterRules);

        if (options.TransformRules?.Count > 0)
            table = ApplyTransforms(table, options.TransformRules);

        if (options.AggregationRules?.Count > 0)
            table = ApplyAggregation(table, options.AggregationRules);

        return table;
    }

    public async Task<ProcessingResult> ProcessAsync(ProcessingOptions options)
    {
        try
        {
            if (!File.Exists(options.InputPath))
                return ProcessingResult.FileNotFound;

            var csvReader = new CsvReader();
            var table = csvReader.Read(options.InputPath);

            table = Execute(table, options);

            CsvWriter.Write(options.OutputPath, table);

            return ProcessingResult.Success;
        }
        catch (IOException)
        {
            return ProcessingResult.FileNotFound;
        }
        catch
        {
            return ProcessingResult.ProcessingError;
        }
    }


    public DataTableModel Process(DataTableModel table)
    {
        return table;
    }
    private static bool Evaluate(string cellValue, FilterRule rule)
    {
        switch (rule.Operator)
        {
            case FilterOperator.Eq:
                return cellValue == rule.Value;

            case FilterOperator.Neq:
                return cellValue != rule.Value;

            case FilterOperator.Contains:
                return cellValue.Contains(rule.Value, StringComparison.OrdinalIgnoreCase);

            case FilterOperator.StartsWith:
                return cellValue.StartsWith(rule.Value, StringComparison.OrdinalIgnoreCase);

            case FilterOperator.EndsWith:
                return cellValue.EndsWith(rule.Value, StringComparison.OrdinalIgnoreCase);

            case FilterOperator.Gt:
                return decimal.TryParse(cellValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var gtLeft) &&
                       decimal.TryParse(rule.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var gtRight) &&
                       gtLeft > gtRight;

            case FilterOperator.Gte:
                return decimal.TryParse(cellValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var gteLeft) &&
                       decimal.TryParse(rule.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var gteRight) &&
                       gteLeft >= gteRight;

            case FilterOperator.Lt:
                return decimal.TryParse(cellValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var ltLeft) &&
                       decimal.TryParse(rule.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var ltRight) &&
                       ltLeft < ltRight;

            case FilterOperator.Lte:
                return decimal.TryParse(cellValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var lteLeft) &&
                       decimal.TryParse(rule.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var lteRight) &&
                       lteLeft <= lteRight;

            default:
                return false;
        }
    }
    public DataTableModel ApplyFilters(
    DataTableModel table,
    IReadOnlyList<FilterRule> rules)
    {
        if (rules == null || rules.Count == 0)
            return table;

        var result = new DataTableModel(table.Headers);

        foreach (var row in table.Rows)
        {
            bool include = true;

            foreach (var rule in rules)
            {
                if (!row.ContainsKey(rule.Field))
                {
                    include = false;
                    break;
                }

                var cellValue = row[rule.Field];

                if (!Evaluate(cellValue, rule))
                {
                    include = false;
                    break;
                }
            }

            if (include)
                result.AddRow(new Dictionary<string, string>(row));
        }

        return result;
    }

    public DataTableModel ApplyTransforms(
    DataTableModel table,
    List<TransformRule> rules)
    {
        if (rules == null || rules.Count == 0)
            return table;

        var result = new DataTableModel(table.Headers);

        foreach (var row in table.Rows)
        {
            var newRow = new Dictionary<string, string>(row);

            foreach (var rule in rules)
            {
                if (!newRow.ContainsKey(rule.Column))
                    continue;

                var value = newRow[rule.Column];
                newRow[rule.Column] = Transform(value, rule.Operation);
            }

            result.AddRow(newRow);
        }

        return result;
    }
    public string Transform(string value, string operation)
    {
        switch (operation.ToLower())
        {
            case "upper":
                return value.ToUpperInvariant();

            case "lower":
                return value.ToLowerInvariant();

            case "trim":
                return value.Trim();

            default:
                return value;
        }
    }
    public DataTableModel ApplyAggregation(
    DataTableModel table,
    List<AggregationRule> rules)
    {
        if (rules == null || rules.Count == 0)
            return table;

        var result = new DataTableModel(
            rules.Select(r => $"{r.Function}_{r.Column}").ToList()
        );

        var aggregatedRow = new Dictionary<string, string>();

        foreach (var rule in rules)
        {
            var value = Aggregate(table, rule);
            aggregatedRow[$"{rule.Function}_{rule.Column}"] = value;
        }

        result.AddRow(aggregatedRow);

        return result;
    }
    private string Aggregate(DataTableModel table, AggregationRule rule)
    {
        var values = table.Rows
            .Where(r => r.ContainsKey(rule.Column))
            .Select(r => r[rule.Column])
            .ToList();

        switch (rule.Function.ToLower())
        {
            case "count":
                return values.Count.ToString();

            case "sum":
                return values
                    .Select(v => decimal.TryParse(v,
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out var d) ? d : 0)
                    .Sum()
                    .ToString(System.Globalization.CultureInfo.InvariantCulture);

            case "avg":
                var numbers = values
                    .Select(v => decimal.TryParse(v,
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out var d) ? d : 0)
                    .ToList();

                if (numbers.Count == 0)
                    return "0";

                return (numbers.Sum() / numbers.Count)
                    .ToString(System.Globalization.CultureInfo.InvariantCulture);

            default:
                return "0";
        }
    }


}

