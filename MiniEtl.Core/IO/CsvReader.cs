using MiniEtl.Core.Models;

namespace MiniEtl.Core.IO;

public class CsvReader
{
    public DataTableModel Read(string path)
    {
        var lines = File.ReadAllLines(path);

        if (lines.Length == 0)
            throw new InvalidOperationException("Empty file");

        var headers = lines[0].Split(',').ToList();
        var table = new DataTableModel(headers);

        for (int i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(',');
            var row = new Dictionary<string, string>();

            for (int j = 0; j < headers.Count; j++)
            {
                row[headers[j]] = j < values.Length ? values[j] : "";
            }

            table.AddRow(row);
        }

        return table;
    }
}


