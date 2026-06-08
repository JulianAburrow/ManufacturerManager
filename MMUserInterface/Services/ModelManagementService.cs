namespace MMUserInterface.Services;

public class ModelManagementService : IModelManagementService
{
    public async Task DeleteModelAsync(string modelName)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "ollama",
            Arguments = $"rm {modelName}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(psi)
            ?? throw new InvalidOperationException("Failed to start ollama process.");

        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            var error = await process.StandardError.ReadToEndAsync();
            throw new Exception($"Failed to delete model: {error}");
        }
    }

    public async Task<OllamaModel?> GetModelAsync(string modelName)
    {
        using var apiClient = new HttpClient();
        var tags = await apiClient.GetFromJsonAsync<OllamaTags>($"{MMBusinessLayer.Shared.CommonValues.SharedValues.ApiAddress}/tags");
        if (tags?.Models is null)
            return null;

        var match = tags.Models
            .FirstOrDefault(m => m.Name.Equals(modelName, StringComparison.OrdinalIgnoreCase));

        if (match is null)
            return null;

        return new OllamaModel
        {
            Name = match.Name,
            Size = match.Size,
            ModifiedAt = match.ModifiedAt == default
                ? DateTime.MinValue
                : match.ModifiedAt,
        };
    }

    public async Task<List<OllamaModel>> GetAvailableModelsAsync()
    {
        var apiClient = new HttpClient();
        var response = await apiClient.GetFromJsonAsync<OllamaTags>($"{MMBusinessLayer.Shared.CommonValues.SharedValues.ApiAddress}/tags");
        return (response?.Models ?? [])
            .OrderBy(m => m.Name)
            .ToList();
    }
}
