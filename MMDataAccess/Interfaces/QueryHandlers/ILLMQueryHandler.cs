namespace MMDataAccess.Interfaces.QueryHandlers;

public interface ILLMQueryHandler
{
    Task<List<OllamaModel>> GetLLMSAsync();

    Task<OllamaModel> GetLLMAsync(string name);
}
