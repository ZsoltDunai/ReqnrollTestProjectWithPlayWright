using Reqnroll;

namespace ReqnrollTestProject.Extensions;

public static class TableExtensions
{
    public static Dictionary<string, string> ToDictionary(this DataTable table)
    {
        var columnHeaders = table.Header.ToList();

        if (columnHeaders.Count != 2)
            throw new Exception($"The given input table has {columnHeaders.Count} columns!");

        if (columnHeaders[0] != "Key" || columnHeaders[1] != "Value")
            throw new Exception("Please check the table headers! The first should be 'Key' and the second is 'Value'");

        return table.Rows.ToDictionary(row => row[0], row => row[1]);
    }
}
