namespace MMUserInterface.Components.Pages.MyMMs;

public partial class View
{
    protected override async Task OnInitializedAsync()
    {
        MyMMModel = await MyMMQueryHandler.GetMyMMAsync(MyMMId);
        MainLayout.SetHeaderValue("View MyMM");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetMyMMHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(ViewTextForBreadcrumb),
        ]);
    }
}