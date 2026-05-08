namespace MMUserInterface.Shared.BasePageClasses;

public class LLMBasePageClass : BasePageClass
{
    [Inject] protected IChatService ChatService { get; set; } = default!;

    protected List<OllamaModel>? LLMs;

    protected OllamaModel? OllamaModel { get; set; } = new();

    [Parameter] public string LLMName { get; set; } = string.Empty;

    protected string LLMSingular = "LLM";

    protected string LLMPlural = "LLMs";

    protected BreadcrumbItem GetLLMHomeBreadcrumbItem(bool isDisabled = false)
    {
        return new(LLMPlural, "/llms/index", isDisabled);
    }
}
