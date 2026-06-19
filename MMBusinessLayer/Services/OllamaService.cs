namespace MMBusinessLayer.Services;

public class OllamaService(HttpClient httpClient) : ILlmClient
{
    public async Task<string> GenerateAsync(string prompt)
    {
        return await GenerateAsync(prompt, "llama3.1", true);
    }

    public async Task<string> GenerateAsync(string prompt, string model, bool useStreaming)
    {
        try
        {
            var client = httpClient;
            var request = new
            {
                model,
                prompt,
                stream = useStreaming,
            };

            var response = await client.PostAsJsonAsync($"{SharedValues.ApiAddress}/generate", request);
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

                        // If the last char is alphanumeric AND the next fragment starts with alphanumeric,
                        // insert a space to avoid glued tokens like "ORDER BYm.Name"
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
                    // Could log the exception here if needed
                }
            }

            return outputBuilder.Length > 0
                ? StripCodeFences(outputBuilder.ToString())
                : "No response received.";
        }
        catch (HttpRequestException)
        {
            // Ollama not installed, not running, or endpoint unreachable
            return "REFUSAL: Unable to retrieve available models. Please ensure that Ollama is installed and running and that at least one LLM is present.";
        }
        catch (Exception)
        {
            // Any other unexpected failure
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
