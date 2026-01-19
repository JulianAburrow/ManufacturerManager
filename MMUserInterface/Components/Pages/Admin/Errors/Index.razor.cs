namespace MMUserInterface.Components.Pages.Admin.Errors;

public partial class Index
{
    private List<ErrorModel>? Errors { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Errors = await ErrorQueryHandler.GetErrorsAsync();
        var count = Errors.Count;
        Snackbar.Add($"{count} error{(count == 1 ? "" : "s")} found", count > 0 ? Severity.Info : Severity.Warning);
        MainLayout.SetHeaderValue("Errors");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetErrorHomeBreadcrumbItem(true),
        ]);
    }
}