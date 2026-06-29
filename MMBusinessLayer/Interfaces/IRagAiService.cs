namespace MMBusinessLayer.Interfaces;

public interface IRagAiService
{
    string BuildPrompt(string document, string question, string languageRequired, bool strictMode);

    Task<string> AskQuestionAsync(string category, string question, string model, string languageRequired, bool strictMode = true);
}
