namespace MMUserInterface.Components.Pages.Admin.Colours;

public partial class Delete
{
    protected override async Task OnInitializedAsync()
    {
        ColourModel = await ColourQueryHandler.GetColourAsync(ColourId);
        MainLayout.SetHeaderValue("Delete Colour");
        OkToDelete = ColourModel.Widgets.Count == 0;
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetColourHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(DeleteTextForBreadcrumb),
        ]);
    }

    private async Task DeleteColour()
    {
        try
        {
            await ColourCommandHandler.DeleteColourAsync(ColourId, true);
            Snackbar.Add($"Colour {ColourModel.Name} successfully deleted", Severity.Success);
            NavigationManager.NavigateTo("/colours/index");
        }
        catch (Exception ex)
        {
            Snackbar.Add($"An error occurred deleting colour {ColourModel.Name}. Please try again", Severity.Error);
            await ErrorCommandHandler.CreateErrorAsync(ex, true);
        }
    }
}
