namespace MMUserInterface.Interfaces;

public interface IChatService
{
    Task<string> AskQuestionAsync(string category, string question, string searchModel, string languageRequired, bool strictMode = true);

    IReadOnlyList<string> GetMatchingFiles(string category);

    Task<List<OllamaModel>> GetAvailableModelsAsync();

}
