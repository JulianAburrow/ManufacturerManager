namespace MMUserInterface.Components.Pages;

public partial class Home
{
    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(isDisabled: true)
        ]);
    }
}