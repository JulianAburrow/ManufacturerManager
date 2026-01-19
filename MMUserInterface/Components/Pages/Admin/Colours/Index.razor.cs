namespace MMUserInterface.Components.Pages.Admin.Colours;

public partial class Index
{
    private List<ColourModel>? Colours { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Colours = await ColourQueryHandler.GetColoursAsync();
        var count = Colours.Count;
        Snackbar.Add($"{count} colour{(count == 1 ? "" : "s")} found", count > 0 ? Severity.Info : Severity.Warning);
        MainLayout.SetHeaderValue("Colours");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetColourHomeBreadcrumbItem(true),
        ]);
    }
}
