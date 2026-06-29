namespace MMBusinessLayer.Services;

public class GroqService : ILlmClient
{
    private readonly HttpClient _http;
    private readonly string _apiKey;
    private readonly string _defaultModel;

    public GroqService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _apiKey = config["Groq:ApiKey"]
            ?? throw new InvalidOperationException("Groq:ApiKey missing from configuration");
        _defaultModel = config["Groq:Model"] ?? "llama-3.1-8b-instant";
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _apiKey);
    }

    public async Task<string> GenerateAsync(string prompt)
    {
        return await GenerateInternalAsync(prompt, _defaultModel);
    }

    public async Task<string> GenerateAsync(string prompt, string? model, bool useStreaming)
    {
        // Groq doesn't support streaming via this endpoint, so ignore useStreaming.
        var chosenModel = string.IsNullOrWhiteSpace(model) ? _defaultModel : model;
        return await GenerateInternalAsync(prompt, chosenModel);
    }

    private async Task<string> GenerateInternalAsync(string prompt, string model)
    {
        var request = new
        {
            model = _defaultModel,
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        };

        var response = await _http.PostAsJsonAsync(
            "https://api.groq.com/openai/v1/chat/completions",
            request);

        //var raw = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();

        return json.GetProperty("choices")[0]
                   .GetProperty("message")
                   .GetProperty("content")
                   .GetString() ?? string.Empty;
    }
}
