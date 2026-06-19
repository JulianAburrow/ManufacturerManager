namespace MMBusinessLayer.Services;

public class TogetherAiService : ILlmClient
{
    private readonly HttpClient _http;
    private readonly string _apiKey;

    public TogetherAiService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _apiKey = config["TogetherAI:ApiKey"]
            ?? throw new InvalidOperationException("TogetherAI:ApiKey missing from configuration");
    }

    public async Task<string> GenerateAsync(string prompt, string model, bool useStreaming)
    {
        return await GenerateAsync(prompt);
    }

    public async Task<string> GenerateAsync(string prompt)
    {
        var request = new TogetherChatRequest
        {
            Model = "meta-llama/Meta-Llama-3.1-70B-Instruct",
            Messages =
            [
                new TogetherMessage
                {
                    Role = "user",
                    Content = prompt,
                }
            ]
        };

        _http.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

        var response = await _http.PostAsJsonAsync("https://api.together.xyz/v1/chat/completions", request);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<TogetherChatResponse>();

        return result?.Choices?.FirstOrDefault()?.Message.Content ?? string.Empty;
    }
}
