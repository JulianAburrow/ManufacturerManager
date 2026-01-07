namespace MMUserInterface.Components.Pages.MyMMs;

public partial class Index
{
    protected override async Task OnInitializedAsync()
    {
        MyMMs = await MyMMQueryHandler.GetMyMMsAsync();
        Snackbar.Add($"{MyMMs.Count} item(s) found.", MyMMs.Count > 0 ? Severity.Info : Severity.Warning);
        MainLayout.SetHeaderValue("MyMMs");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetMyMMHomeBreadcrumbItem(true),
        ]);
    }

    private async Task ExportCSV()
    {
        var csvString = CSVStringHelper.CreateMyMMCSVString(MyMMs);
        var fileBytes = SharedMethods.GetUTF8Bytes(csvString);
        var base64 = SharedMethods.GetBase64String(fileBytes);
        var fileName = $"MyMMs-{DateTime.Now}.csv";
        await JS.InvokeVoidAsync(DownloadFile, base64, ContentType, fileName);
    }
}