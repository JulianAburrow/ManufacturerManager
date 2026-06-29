namespace MMBusinessLayer.Services;

public class OllamaService : ILlmClient
{
    private readonly HttpClient _http;
    private readonly string _defaultmodel;

    public OllamaService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _defaultmodel = config["Ollama:Model"] ?? "qwen2.5:14B";
    }

    // NL→SQL offline mode uses this
    public Task<string> GenerateAsync(string prompt)
    {
        return GenerateAsync(prompt, _defaultmodel, useStreaming: false);
    }

    // RAG offline mode uses this
    public async Task<string> GenerateAsync(string prompt, string? model, bool useStreaming)
    {
        try
        {
            var chosenModel = string.IsNullOrWhiteSpace(model)
                ? _defaultmodel
                : model;

            var request = new
            {
                model = chosenModel,
                prompt,
                stream = useStreaming
            };

            var response = await _http.PostAsJsonAsync($"{SharedValues.ApiAddress}/generate", request);
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
                        string text = fragment.Response;

                        if (outputBuilder.Length > 0 &&
                            char.IsLetterOrDigit(outputBuilder[^1]) &&
                            char.IsLetterOrDigit(text[0]))
                        {
                            outputBuilder.Append(' ');
                        }

                        outputBuilder.Append(text);
                    }
                }
                catch
                {
                    // swallow malformed fragments
                }
            }

            return outputBuilder.Length > 0
                ? StripCodeFences(outputBuilder.ToString())
                : "No response received.";
        }
        catch (HttpRequestException)
        {
            return "REFUSAL: Unable to retrieve available models. Please ensure that Ollama is installed and running and that at least one LLM is present.";
        }
        catch (Exception)
        {
            return "REFUSAL: Unable to retrieve available models. Please ensure that Ollama is installed and running and that at least one LLM is present.";
        }
    }

    private static string StripCodeFences(string sql)
    {
        return sql
            .Replace("```sql", "")
            .Replace("```", "")
            .Trim();
    }
}
