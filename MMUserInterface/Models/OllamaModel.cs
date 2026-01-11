namespace MMUserInterface.Models;

public class OllamaModel
{
    public string Name { get; set; } = string.Empty;

    public long Size { get; set; }

    public string Digest { get; set; } = string.Empty;

    public DateTime ModifiedAt { get; set; }
}
