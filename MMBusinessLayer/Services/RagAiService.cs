namespace MMBusinessLayer.Services;

public class RagAiService(ILlmClient aiService, IDocumentService documentService) : IRagAiService
{    
    public string BuildPrompt(string document, string question, string languageRequired, bool strictMode)
    {
        var builder = new StringBuilder();

        if (strictMode)
        {
            var instructions = PromptHelper.Load(PromptEnum.RagAiPrompt);
            instructions = instructions.Replace("{{LANGUAGE}}", languageRequired);
            builder.AppendLine(instructions);
            builder.AppendLine();
        }

        builder.AppendLine("=== DOCUMENT START ===");
        builder.AppendLine(document.Trim());
        builder.AppendLine("=== DOCUMENT END ===");
        builder.AppendLine();
        builder.AppendLine("=== QUESTION ===");
        builder.AppendLine(question.Trim());

        return builder.ToString();
    }

    public async Task<string> AskQuestionAsync(string category, string question, string model, string languageRequired, bool strictMode = true)
    {
        var files = documentService.GetMatchingFiles(category);
        if (files.Count == 0)
            return $"📂 No documents found for category '{category}'.";

        var combinedText = new StringBuilder();
        foreach (var file in files)
        {
            combinedText.AppendLine(documentService.ExtractTextFromPdf(file));
        }

        string prompt = BuildPrompt($"{category} {combinedText}", question, languageRequired, strictMode);
        return await aiService.GenerateAsync(prompt, model, true);
    }
}
