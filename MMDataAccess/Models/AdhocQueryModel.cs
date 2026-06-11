namespace MMDataAccess.Models;

public class AdhocQueryModel
{
    public int AdhocQueryId { get; set; }

    public string NaturalLanguageQuery { get; set; } = string.Empty;

    public string MessageOrSqlReturned { get; set; } = string.Empty;

    public DateTime WhenRun { get; set; }

    public bool IsSuccessful { get; set; }
}
