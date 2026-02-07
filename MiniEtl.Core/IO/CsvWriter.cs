using MiniEtl.Core.Models;
using System.Text;


namespace MiniEtl.Core.IO;

public class CsvWriter
{
    public static void Write(string path, DataTableModel table)
    {
        var sb = new StringBuilder();

        // Headers
        sb.AppendLine(string.Join(",", table.Headers));

        // Rows
        foreach (var row in table.Rows)
        {
            var values = table.Headers
                .Select(header => row.ContainsKey(header) ? row[header] : "");

            sb.AppendLine(string.Join(",", values));
        }

        File.WriteAllText(path, sb.ToString());
    }
}

