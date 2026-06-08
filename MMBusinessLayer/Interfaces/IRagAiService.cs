namespace MMBusinessLayer.Interfaces;

public interface IRagAiService
{
    IReadOnlyList<string> GetMatchingFiles(string category);

    string ExtractTextFromPdf(string filePath);

    string BuildPrompt(string document, string question, string languageRequired, bool strictMode);

    Task<string> AskQuestionAsync(string category, string question, string model, string languageRequired, bool strictMode = true);
}
