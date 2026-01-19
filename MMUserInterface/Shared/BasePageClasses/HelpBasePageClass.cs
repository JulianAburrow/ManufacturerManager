namespace MMUserInterface.Shared.BasePageClasses;

public abstract class HelpBasePageClass : BasePageClass
{
    [Inject] protected IChatService ChatService { get; set; } = default!;

    protected string HelpCategoryPlural = "Help Categories";
}
