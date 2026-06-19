namespace MMBusinessLayer.Services;

public class NaturalLanguageService(ILlmClient service) : INaturalLanguageService
{
    public Task<string> GetSqlStringFromNaturalQuery(string query)
    {
        var prompt = $"{PromptHelper.Load(PromptEnum.SqlPrompt)}\n\nUser query: {query}";
        return service.GenerateAsync(prompt, "qwen2.5:14B", false);
    }
}
