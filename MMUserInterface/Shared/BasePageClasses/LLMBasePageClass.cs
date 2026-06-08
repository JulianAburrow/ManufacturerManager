using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace MMUserInterface.Shared.BasePageClasses;

public class LLMBasePageClass : BasePageClass
{
    [Inject] protected IModelManagementService ModelManagementService { get; set; } = null!;

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
