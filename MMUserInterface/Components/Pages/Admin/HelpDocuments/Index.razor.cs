namespace MMUserInterface.Components.Pages.Admin.HelpDocuments;

public partial class Index
{
    private List<HelpDocumentModel>? HelpDocumentModels { get; set; }

    protected override async Task OnInitializedAsync()
    {
        HelpDocumentModels = await HelpDocumentService.GetHelpDocumentModelsAsync();
        var count = HelpDocumentModels.Count;
        Snackbar.Add($"{count} help document{(count == 1 ? "" : "s")} found", count > 0 ? Severity.Info : Severity.Warning);
        MainLayout.SetHeaderValue("Help Documents");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetHelpDocumentHomeBreadcrumbItem(true),
        ]);
    }
}