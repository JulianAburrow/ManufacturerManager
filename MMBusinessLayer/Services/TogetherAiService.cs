namespace MMBusinessLayer.Services;

public class TogetherAiService : ILlmClient
{
    private readonly HttpClient _http;
    private readonly string _apiKey;
    private readonly string _defaultModel;

    public TogetherAiService(HttpClient http, IConfiguration config)
    {
        _http = http;

        _apiKey = config["TogetherAI:ApiKey"]
            ?? throw new InvalidOperationException("TogetherAI:ApiKey missing from configuration");

        _defaultModel = config["TogetherAI:Model"]
            ?? "meta-llama/Meta-Llama-3.1-70B-Instruct";

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _apiKey);
    }

    public async Task<string> GenerateAsync(string prompt)
    {
        return await GenerateInternalAsync(prompt, _defaultModel);
    }

    public async Task<string> GenerateAsync(string prompt, string model, bool useStreaming)
    {
        // TogetherAI doesn't support streaming via this endpoint.
        var chosenModel = string.IsNullOrWhiteSpace(model) ? _defaultModel : model;
        return await GenerateInternalAsync(prompt, chosenModel);
    }

    private async Task<string> GenerateInternalAsync(string prompt, string model)
    {
        var request = new TogetherChatRequest
        {
            Model = model,
            Messages =
            [
                new TogetherMessage
                {
                    Role = "user",
                    Content = prompt
                }
            ]
        };

        var response = await _http.PostAsJsonAsync(
            "https://api.together.xyz/v1/chat/completions",
            request);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<TogetherChatResponse>();

        return result?.Choices?.FirstOrDefault()?.Message?.Content ?? string.Empty;
    }
}