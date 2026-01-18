namespace MMUserInterface.Shared.BasePageClasses;

public class BasePageClass : ComponentBase
{
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

    [Inject] protected ISnackbar Snackbar { get; set; } = default!;

    [Inject] protected ICategoryCommandHandler CategoryCommandHandler { get; set; } = default!;

    [Inject] protected ICategoryQueryHandler CategoryQueryHandler { get; set; } = default!;

    [Inject] protected ICrudWithErrorHandlingHelper CrudWithErrorHandlingHelper { get; set; } = default!;

    [Inject] protected IErrorCommandHandler ErrorCommandHandler { get; set; } = default!;

    [Inject] protected IErrorQueryHandler ErrorQueryHandler { get; set; } = default!;

    [Inject] protected ICSVStringHelper CSVStringHelper { get; set; } = default!;

    [Inject] protected IJSRuntime JS { get; set; } = null!;

    protected string ContentType = "application/octet-stream";

    protected string DownloadFile = "downloadFile";

    [CascadingParameter] public MainLayout MainLayout { get; set; } = new();

    protected string Values = "Values";

    protected BreadcrumbItem GetHomeBreadcrumbItem(bool isDisabled = false)
    {
        return new("Home", "#", isDisabled, icon: Icons.Material.Filled.Home);
    }

    protected BreadcrumbItem GetCustomBreadcrumbItem(string text)
    {
        return new(text, null, true);
    }

    protected string CreateTextForBreadcrumb = "Create";

    protected string DeleteTextForBreadcrumb = "Delete";

    protected string EditTextForBreadcrumb = "Edit";

    protected string ViewTextForBreadcrumb = "View";

    protected bool OkToDelete { get; set; }
}
