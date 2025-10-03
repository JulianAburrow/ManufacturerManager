namespace MMUserInterface.Components.Pages.Admin.HelpDocuments;

public partial class Index
{
    protected List<HelpDocumentModel> HelpDocumentModels { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        HelpDocumentModels = await HelpDocumentService.GetHelpDocumentModelsAsync();
        Snackbar.Add($"{HelpDocumentModels.Count} item(s) found", HelpDocumentModels.Count > 0 ? Severity.Info : Severity.Warning);
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