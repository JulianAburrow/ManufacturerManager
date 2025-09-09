namespace MMUserInterface.Shared.BasePageClasses;

public class HelpBasePageClass : BasePageClass
{
    [Inject] protected IChatService ChatService { get; set; } = default!;
}
