namespace MMBusinessLayer.Interfaces;

public interface ILlmClient
{
    Task<string> GenerateAsync(string prompt);

    Task<string> GenerateAsync(string prompt, string model, bool useStreaming);
}
