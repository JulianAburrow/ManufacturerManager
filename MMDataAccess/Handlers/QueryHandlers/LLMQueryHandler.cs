namespace MMDataAccess.Handlers.QueryHandlers;

public class LLMQueryHandler : ILLMQueryHandler
{
    public Task<OllamaModel> GetLLMAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<List<OllamaModel>> GetLLMSAsync()
    {
        throw new NotImplementedException();
    }
}
