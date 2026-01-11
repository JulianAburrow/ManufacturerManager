namespace MMUserInterface.Services;

public class ChatService : IChatService
{
    private static readonly string apiAddress = "http://localhost:11434/api";

    public IReadOnlyList<string> GetMatchingFiles(string category)
    {
        string folderPath = Path.Combine("Documents", category);
        return Directory.GetFiles(folderPath)
                        .Where(f => Path.GetFileName(f)
                        .Contains(category, StringComparison.OrdinalIgnoreCase))
                        .ToList();
    }

    public async Task<string> AskQuestionAsync(string category, string question, string model, bool strictMode = true)
    {
        var files = GetMatchingFiles(category);
        if (files.Count == 0)
            return $"📂 No documents found for category '{category}'.";

        var combinedText = new StringBuilder();
        foreach (var file in files)
        {
            combinedText.AppendLine(ExtractTextFromPdf(file));
        }

        string prompt = BuildPrompt($"{category} {combinedText}", question, strictMode);
        return await QueryOllamaAsync(prompt, model);
    }

    public async Task<List<OllamaModel>> GetAvailableModelsAsync()
    {
        var apiClient = new HttpClient();
        var response = await apiClient.GetFromJsonAsync<OllamaTags>($"{apiAddress}/tags");
        return (response?.Models ?? [])
            .OrderBy(m => m.Name)
            .ToList();
    }

    private static string ExtractTextFromPdf(string filePath)
    {
        using var pdf = PdfDocument.Open(filePath);
        var text = new StringBuilder();
        foreach (var page in pdf.GetPages())
        {
            text.AppendLine(page.Text);
        }
        return text.ToString();
    }

    private static string BuildPrompt(string document, string question, bool strictMode)
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
            builder.AppendLine();
        }

        builder.AppendLine("=== DOCUMENT START ===");
        builder.AppendLine(document.Trim());
        builder.AppendLine("=== DOCUMENT END ===");
        builder.AppendLine();
        builder.AppendLine("=== QUESTION ===");
        builder.AppendLine(question.Trim());

        //if (strictMode)
        //{
        //    builder.AppendLine("You are an assistant that answers using only the information found in the document.");
        //    builder.AppendLine("If the document does not contain the answer, say so briefly.");
        //    builder.AppendLine("Keep your answer concise and factual.");
        //    builder.AppendLine("Use bullet points when listing items.");
        //    builder.AppendLine();
        //}
        //else
        //{
        //    builder.AppendLine("You are a concise, helpful assistant.");
        //    builder.AppendLine("If the question is unclear, state your assumption briefly and answer based on it.");
        //    builder.AppendLine("Keep explanations short and direct.");
        //    builder.AppendLine();
        //}

        //builder.AppendLine("### Document");
        //builder.AppendLine(document.Trim());
        //builder.AppendLine();

        //builder.AppendLine("### Question");
        //builder.AppendLine(question.Trim());
        //builder.AppendLine();

        //builder.AppendLine("### Answer");

        return builder.ToString();
    }

    private static async Task<string> QueryOllamaAsync(string prompt, string model)
    {
        var client = new HttpClient();
        var request = new
        {
            model,
            prompt,
            stream = true
        };

        var response = await client.PostAsJsonAsync($"{apiAddress}/generate", request);
        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(stream);

        var outputBuilder = new StringBuilder();
        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            try
            {
                var fragment = JsonSerializer.Deserialize<OllamaResponse>(line);
                if (!string.IsNullOrWhiteSpace(fragment?.Response))
                {
                    outputBuilder.Append(fragment.Response);
                }
            }
            catch
            {
                // Could log the exception here if needed
            }
        }

        return outputBuilder.Length > 0 ? outputBuilder.ToString() : "No response received.";
    }
}
