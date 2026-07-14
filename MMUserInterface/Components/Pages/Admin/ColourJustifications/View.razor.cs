namespace MMUserInterface.Components.Pages.Admin.ColourJustifications;

public partial class View
{
    protected override async Task OnInitializedAsync()
    {
        ColourJustificationModel = await ColourJustificationQueryHandler.GetColourJustificationAsync(ColourJustificationId);

        if (ColourJustificationModel.ColourJustificationId == 0)
        {
            MainLayout.SetHeaderValue(ColourJustificationNotFoundMessage);
            _entityNotFound = true;
            return;
        }

        MainLayout.SetHeaderValue("View Colour Justification");
        OkToDelete = ColourJustificationModel.Widgets.Count == 0;

        _isLoaded = true;
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetColourJustificationHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(ViewTextForBreadcrumb),
        ]);
    }
}
