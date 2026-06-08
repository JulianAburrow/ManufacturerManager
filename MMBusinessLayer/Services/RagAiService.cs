namespace MMBusinessLayer.Services;

public class RagAiService(IOllamaService ollamaService) : IRagAiService
{
    private readonly IOllamaService _ollamaService = ollamaService;

    public IReadOnlyList<string> GetMatchingFiles(string category)
    {
        string folderPath = Path.Combine("Documents", category);
        return Directory.GetFiles(folderPath)
                        .Where(f => Path.GetFileName(f)
                        .Contains(category, StringComparison.OrdinalIgnoreCase))
                        .ToList();
    }

    public string ExtractTextFromPdf(string filePath)
    {
        using var pdf = PdfDocument.Open(filePath);
        var text = new StringBuilder();
        foreach (var page in pdf.GetPages())
        {
            text.AppendLine(page.Text);
        }
        return text.ToString();
    }

    public string BuildPrompt(string document, string question, string languageRequired, bool strictMode)
    {
        var builder = new StringBuilder();

        if (strictMode)
        {
            builder.AppendLine("=== INSTRUCTIONS ===");
            builder.AppendLine("You are a document-aware assistant.");
            builder.AppendLine("Only use information explicitly stated in the document.");
            builder.AppendLine("Do not infer, guess, or elaborate beyond the source material.");
            builder.AppendLine("Do not rely on general knowledge or external assumptions.");
            builder.AppendLine("Respond in bullet points only.");
            builder.AppendLine("Avoid conversational filler or introductory phrases.");
            builder.AppendLine("Do not summarize or restate the document’s implications.");
            builder.AppendLine("Only reproduce explicit steps or facts.");
            builder.AppendLine("Do not describe post-creation behavior unless explicitly stated in the document.");
            builder.AppendLine($"Please translate your responses into {languageRequired} language.");
            builder.AppendLine($"If you don't understand or cannot translate {languageRequired} please say so and do not attempt any other answer");
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
        var files = GetMatchingFiles(category);
        if (files.Count == 0)
            return $"📂 No documents found for category '{category}'.";

        var combinedText = new StringBuilder();
        foreach (var file in files)
        {
            combinedText.AppendLine(ExtractTextFromPdf(file));
        }

        string prompt = BuildPrompt($"{category} {combinedText}", question, languageRequired, strictMode);
        return await _ollamaService.GenerateAsync(prompt, model, true);
    }
}
