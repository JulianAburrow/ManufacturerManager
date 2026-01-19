namespace MMUserInterface.Components.Pages.Admin.ColourJustifications;

public partial class Index
{
    private List<ColourJustificationModel>? ColourJustifications { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ColourJustifications = await ColourJustificationQueryHandler.GetColourJustificationsAsync();
        var count = ColourJustifications.Count;
        Snackbar.Add($"{count} colour justification{(count == 1 ? "" : "s")} found", count > 0 ? Severity.Info : Severity.Warning);
        MainLayout.SetHeaderValue("Colour Justifications");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetColourJustificationHomeBreadcrumbItem(true),
        ]);
    }
}