namespace MMUserInterface.Components.Pages;

public partial class Home
{
    protected override async Task OnInitializedAsync()
    {
        MyMMs = await MyMMQueryHandler.GetMyMMsForHomePageAsync();
        Snackbar.Add($"{MyMMs.Count} item(s) found.", MyMMs.Count > 0 ? Severity.Info : Severity.Warning);
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