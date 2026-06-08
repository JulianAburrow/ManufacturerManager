namespace MMBusinessLayer.Interfaces;

public interface IOllamaService
{
    Task<string> GenerateAsync(string prompt, string model, bool useStreaming);
}
