namespace MMUserInterface.Shared.BasePageClasses;

public class HelpDocumentBasePageClass : BasePageClass
{
    [Inject] protected IHelpDocumentService HelpDocumentService { get; set; } = default!;

    protected string HelpDocument = "Help Document";

    protected string HelpDocumentPlural = "Help Documents";

    protected BreadcrumbItem GetHelpDocumentHomeBreadcrumbItem(bool isDisabled = false)
    {
        return new(HelpDocumentPlural, "/helpdocuments/index", isDisabled);
    }
}
