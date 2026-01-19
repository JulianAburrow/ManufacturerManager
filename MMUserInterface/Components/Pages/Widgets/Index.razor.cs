namespace MMUserInterface.Components.Pages.Widgets;

public partial class Index
{
    private List<WidgetSummary>? Widgets { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Widgets = await WidgetQueryHandler.GetWidgetsAsync();
        var count = Widgets.Count;
        Snackbar.Add($"{count} widget{(count == 1 ? "" : "s")} found.", count > 0 ? Severity.Info : Severity.Warning);
        MainLayout.SetHeaderValue("Widgets");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetWidgetHomeBreadcrumbItem(true),
        ]);
    }

    private async Task ExportCSV()
    {
        if (Widgets == null || !Widgets.Any())
        {
            Snackbar.Add("No widgets available to export.", Severity.Warning);
            return;
        }

        var csvString = CSVStringHelper.CreateWidgetCSVString(Widgets);
        var fileBytes = SharedMethods.GetUTF8Bytes(csvString);
        var base64 = SharedMethods.GetBase64String(fileBytes);
        var fileName = $"Widgets-{DateTime.Now}.csv";

        await JS.InvokeVoidAsync(DownloadFile, base64, ContentType, fileName);
    }
}