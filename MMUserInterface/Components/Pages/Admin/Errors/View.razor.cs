
namespace MMUserInterface.Components.Pages.Admin.Errors;

public partial class View
{
    protected override async Task OnInitializedAsync()
    {
        ErrorModel = await ErrorQueryHandler.GetErrorAsync(ErrorId);

        if (ErrorModel.ErrorId == 0)
        {
            MainLayout.SetHeaderValue(ErrorNotFoundMessage);
            _entityNotFound = true;
            return;
        }

        MainLayout.SetHeaderValue("View Error");

        _isLoaded = true;
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetErrorHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(ViewTextForBreadcrumb),
        ]);
    }
}