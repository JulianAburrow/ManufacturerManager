namespace MMBusinessLayer.Services;

public class OpenRouterService : ILlmClient
{
    private readonly HttpClient _http;
    private readonly string _apiKey;
    private readonly string _defaultModel;

    public OpenRouterService(HttpClient http, IConfiguration config)
    {
        _http = http;

        _apiKey = config["OpenRouter:ApiKey"]
            ?? throw new InvalidOperationException("OpenRouter:ApiKey missing from configuration");

        _defaultModel = config["OpenRouter:Model"] ?? "deepseek-r1-distill-llama-70b";

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _apiKey);

        // These two headers are required by OpenRouter for routing and analytics
        _http.DefaultRequestHeaders.Add("HTTP-Referer", "https://manufacturer-manager.local");
        _http.DefaultRequestHeaders.Add("X-Title", "ManufacturerManager");
    }

    public async Task<string> GenerateAsync(string prompt)
    {
        return await GenerateInternalAsync(prompt, _defaultModel);
    }

    public async Task<string> GenerateAsync(string prompt, string? model, bool useStreaming)
    {
        // We ignore streaming here — OpenRouter supports it, but your interface doesn't require it.
        var chosenModel = string.IsNullOrWhiteSpace(model) ? _defaultModel : model;
        return await GenerateInternalAsync(prompt, chosenModel);
    }

    private async Task<string> GenerateInternalAsync(string prompt, string model)
    {
        var request = new
        {
            model = model,
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        };

        using var msg = new HttpRequestMessage(
            HttpMethod.Post,
            "https://openrouter.ai/api/v1/chat/completions")
        {
            Content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json")
        };

        var response = await _http.SendAsync(msg);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();

        return json.GetProperty("choices")[0]
                   .GetProperty("message")
                   .GetProperty("content")
                   .GetString() ?? string.Empty;
    }
}
