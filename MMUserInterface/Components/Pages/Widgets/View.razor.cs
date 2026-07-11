namespace MMUserInterface.Components.Pages.Widgets;

public partial class View
{
    protected override async Task OnInitializedAsync()
    {
        WidgetModel = await WidgetQueryHandler.GetWidgetAsync(WidgetId);
        MainLayout.SetHeaderValue("View Widget");
        
        _isLoaded = true;
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetWidgetHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(ViewTextForBreadcrumb),
        ]);
    }
}
