namespace MiniEtl.Core.Models;

public class DataTableModel
{
    public List<string> Headers { get; set; }
    public List<Dictionary<string, string>> Rows { get; set; } = new();

    public DataTableModel()
    {
        Headers = new List<string>();
        Rows = new List<Dictionary<string, string>>();
    }
    public DataTableModel(List<string> headers)
    {
        Headers = headers;
        Rows = new List<Dictionary<string, string>>();
    }
    public void AddRow(Dictionary<string, string> row)
    {
        Rows.Add(row);
    }


}

