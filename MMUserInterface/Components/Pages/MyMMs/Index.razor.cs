namespace MMUserInterface.Components.Pages.MyMMs;

public partial class Index
{
    protected override async Task OnInitializedAsync()
    {
        MyMMs = await MyMMQueryHandler.GetMyMMsAsync();
        var count = MyMMs.Count;
        Snackbar.Add($"{count} MyMM{(count == 1 ? "" : "s")} found.", count > 0 ? Severity.Info : Severity.Warning);
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
        if (MyMMs is null || MyMMs.Count == 0)
        {
            Snackbar.Add("No MyMMs to export.", Severity.Warning);
            return;
        }

        var csvString = CSVStringHelper.CreateMyMMCSVString(MyMMs);
        var fileBytes = SharedMethods.GetUTF8Bytes(csvString);
        var base64 = SharedMethods.GetBase64String(fileBytes);
        var fileName = $"MyMMs-{DateTime.Now}.csv";
        await JS.InvokeVoidAsync(DownloadFile, base64, ContentType, fileName);
    }
}