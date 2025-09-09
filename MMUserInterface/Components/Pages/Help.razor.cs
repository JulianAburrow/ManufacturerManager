namespace MMUserInterface.Components.Pages;

public partial class Help
{
    readonly List<string> HelpCategories = [ SharedValues.PleaseSelectText, "Manufacturer", "Widget", "Colour", "ColourJustification" ];

    protected ChatSearchModel ChatSearchModel = new();

    protected string _response = string.Empty;

    private bool _isThinking = false;

    protected List<string> MatchingFiles = new();


    protected override void OnInitialized()
    {
        ChatSearchModel.SearchCategory = HelpCategories[0];
    }

    protected async Task OnSearchClicked()
    {
        _response = string.Empty;
        _isThinking = true;

        if (ChatSearchModel.SearchCategory == SharedValues.PleaseSelectText || string.IsNullOrWhiteSpace(ChatSearchModel.SearchQuestion))
        {
            _response = "❌ Please select a category and enter a question.";
            return;
        }

        try
        {
            string folderPath = Path.Combine("Documents", ChatSearchModel.SearchCategory);
            MatchingFiles = Directory.GetFiles(folderPath)
                                         .Where(f => Path.GetFileName(f)
                                         .Contains(ChatSearchModel.SearchCategory, StringComparison.OrdinalIgnoreCase))
                                         .ToList();

            if (MatchingFiles.Count == 0)
            {
                _response = $"📂 No documents found for category '{ChatSearchModel.SearchCategory}'.";
                return;
            }

            var combinedText = new StringBuilder();
            foreach (var file in MatchingFiles)
            {
                combinedText.AppendLine(ExtractTextFromPdf(file));
            }

            string prompt = BuildPrompt($"{ChatSearchModel.SearchCategory} {combinedText}", ChatSearchModel.SearchQuestion, strictMode: true);
            _response = await QueryOllamaAsync(prompt);
        }
        finally
        {
            _isThinking = false;
            StateHasChanged();
        }
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

        return builder.ToString();
    }

    private static async Task<string> QueryOllamaAsync(string prompt)
    {
        var client = new HttpClient();
        var request = new
        {
            model = "tinyllama",
            prompt,
            stream = false
        };

        var response = await client.PostAsJsonAsync("http://localhost:11434/api/generate", request);
        var result = await response.Content.ReadFromJsonAsync<OllamaResponse>();
        return result?.Response ?? "No response received.";
    }

    public class OllamaResponse
    {
        public string Response { get; set; } = string.Empty;
        public bool Done { get; set; }
    }



}
