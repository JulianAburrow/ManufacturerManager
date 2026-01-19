namespace MMUserInterface.Components.Pages.Manufacturers;

public partial class Index
{
    private List<ManufacturerSummary>? Manufacturers { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Manufacturers = await ManufacturerQueryHandler.GetManufacturersAsync();
        var count = Manufacturers.Count;
        Snackbar.Add($"{count} manufacturer{(count == 1 ? "" : "s")} found.", count > 0 ? Severity.Info : Severity.Warning);
        MainLayout.SetHeaderValue("Manufacturers");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetManufacturerHomeBreadcrumbItem(true),
        ]);
    }

    private async Task ExportCSV()
    {
        if (Manufacturers is null || Manufacturers.Count == 0)
        {
            Snackbar.Add("No manufacturers to export.", Severity.Warning);
            return;
        }

        var csvString = CSVStringHelper.CreateManufacturerCSVString(Manufacturers);
        var fileBytes = SharedMethods.GetUTF8Bytes(csvString);
        var base64 = SharedMethods.GetBase64String(fileBytes);
        var fileName = $"Manufacturers-{DateTime.Now}.csv";

        await JS.InvokeVoidAsync(DownloadFile, base64, ContentType, fileName);
    }
}