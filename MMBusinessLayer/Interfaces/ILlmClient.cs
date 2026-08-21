namespace MMBusinessLayer.Interfaces;

public interface ILlmClient
{
    Task<string> GenerateAsync(
        string prompt,
        CancellationToken cancellationToken,
        string? modelOverride = null,
        bool useStreaming = false);
}