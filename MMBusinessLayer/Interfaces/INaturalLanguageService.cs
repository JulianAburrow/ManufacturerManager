namespace MMBusinessLayer.Interfaces;

public interface INaturalLanguageService
{
    Task<string> GetSqlStringFromNaturalQuery(string query);
}
