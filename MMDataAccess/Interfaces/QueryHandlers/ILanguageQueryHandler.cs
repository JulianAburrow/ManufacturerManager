namespace MMDataAccess.Interfaces.QueryHandlers;

public interface ILanguageQueryHandler
{
    Task<List<LanguageModel>> GetLanguagesAsync();

    Task<List<LanguageModel>> GetLanguagesForHelpPageAsync();
}
