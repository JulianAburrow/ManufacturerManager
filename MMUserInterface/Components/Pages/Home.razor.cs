namespace MMUserInterface.Components.Pages;

public partial class Home
{
    protected override async Task OnInitializedAsync()
    {
        MyMMs = await MyMMQueryHandler.GetMyMMsForHomePageAsync();
        var count = MyMMs.Count;
        Snackbar.Add($"{count} MyMM{(count == 1 ? "" : "s")} found.", count > 0 ? Severity.Info : Severity.Warning);
        MainLayout.SetHeaderValue("Home");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(isDisabled: true)
        ]);
    }
}