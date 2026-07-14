namespace MMUserInterface.Components.Pages.MyMMs;

public partial class View
{
    protected override async Task OnInitializedAsync()
    {
        MyMMModel = await MyMMQueryHandler.GetMyMMAsync(MyMMId);

        if (MyMMModel.MyMMId == 0)
        {
            MainLayout.SetHeaderValue(MyMMNotFoundMessage);
            _entityNotFound = true;
            return;
        }

        MainLayout.SetHeaderValue("View MyMM");

        _isLoaded = true;
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