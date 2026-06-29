namespace MMBusinessLayer.Interfaces;

public interface ILlmClient
{
    Task<string> GenerateAsync(
        string prompt,
        string? modelOverride = null,
        bool useStreaming = false);
}