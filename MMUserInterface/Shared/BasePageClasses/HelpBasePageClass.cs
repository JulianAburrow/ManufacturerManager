using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace MMUserInterface.Shared.BasePageClasses;

public abstract class HelpBasePageClass : BasePageClass
{
    [Inject] protected IModelManagementService ModelManagementService { get; set; } = default!;

    [Inject] protected IRagAiService RagAiService { get; set; } = null!;

    protected string HelpCategoryPlural = "Help Categories";
}
