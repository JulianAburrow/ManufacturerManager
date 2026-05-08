namespace MMUserInterface.Shared.Components;

public partial class LLMGridViewComponent
{
    [Parameter] public List<OllamaModel> LLMs { get; set; } = null;
}