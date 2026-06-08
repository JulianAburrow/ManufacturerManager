namespace MMUserInterface.Shared.BasePageClasses;

public class AdhocQueryBasePageClass : BasePageClass
{
    [Inject] protected IMcpService McpService { get; set; } = default!;
}