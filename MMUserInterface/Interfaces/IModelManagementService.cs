using MMBusinessLayer.Models.Ollama;

namespace MMUserInterface.Interfaces;

public interface IModelManagementService
{
    Task<OllamaModel?> GetModelAsync(string modelName);

    Task DeleteModelAsync(string modelName);

    Task<List<OllamaModel>> GetAvailableModelsAsync();
}
