namespace MMBusinessLayer.Models.Ollama;

public class OllamaModel
{
    public string Name { get; set; } = string.Empty;

    public long Size { get; set; }

    public DateTime ModifiedAt { get; set; }
}
