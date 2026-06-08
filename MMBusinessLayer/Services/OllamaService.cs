namespace MMBusinessLayer.Services;

public class OllamaService(HttpClient httpClient) : IOllamaService
{
    public async Task<string> GenerateAsync(string prompt, string model, bool useStreaming)
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

        return outputBuilder.Length > 0 ? StripCodeFences(outputBuilder.ToString()) : "No response received.";
    }

    private static string StripCodeFences(string sql)
    {
        return sql
            .Replace("```sql", "")
            .Replace("```", "")
            .Trim();
    }
}
