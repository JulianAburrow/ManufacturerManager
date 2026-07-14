namespace MMUserInterface.Components.Pages.Admin.Colours;

public partial class View
{
    protected override async Task OnInitializedAsync()
    {
        ColourModel = await ColourQueryHandler.GetColourAsync(ColourId);

        if (ColourModel.ColourId == 0)
        {
            MainLayout.SetHeaderValue(ColourNotFoundMessage);
            _entityNotFound = true;
            return;
        }

        MainLayout.SetHeaderValue("View Colour");
        OkToDelete = ColourModel.Widgets.Count == 0;

        _isLoaded = true;
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetColourHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(ViewTextForBreadcrumb),
        ]);
    }
}
