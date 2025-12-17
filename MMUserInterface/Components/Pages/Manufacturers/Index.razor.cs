namespace MMUserInterface.Components.Pages.Manufacturers;

public partial class Index
{
    protected List<ManufacturerSummary> Manufacturers { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        Manufacturers = await ManufacturerQueryHandler.GetManufacturersAsync();
        Snackbar.Add($"{Manufacturers.Count} item(s) found.", Manufacturers.Count > 0 ? Severity.Info : Severity.Warning);
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
        var csvString = CSVStringHelper.CreateManufacturerCSVString(Manufacturers);
        var fileBytes = SharedMethods.GetUTF8Bytes(csvString);
        var base64 = SharedMethods.GetBase64String(fileBytes);
        var fileName = $"Manufacturers-{DateTime.Now}.csv";

        await JS.InvokeVoidAsync(DownloadFile, base64, ContentType, fileName);
    }
}