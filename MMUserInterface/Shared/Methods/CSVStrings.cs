namespace MMUserInterface.Shared.Methods;

public static class CSVStrings
{
    public static string EscapeCsv(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return "";
        if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
            return $"\"{value.Replace("\"", "\"\"")}\"";
        return value;
    }
}
