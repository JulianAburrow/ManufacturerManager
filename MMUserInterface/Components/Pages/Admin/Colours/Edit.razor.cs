namespace MMUserInterface.Components.Pages.Admin.Colours;

public partial class Edit
{
    protected override async Task OnInitializedAsync()
    {
        ColourModel = await ColourQueryHandler.GetColourAsync(ColourId);
        CopyModelToDisplayModel();
        MainLayout.SetHeaderValue("Edit Colour");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetColourHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(EditTextForBreadcrumb),
        ]);
    }

    private async Task UpdateColour()
    {
        CopyDisplayModelToModel();

        try
        {
            await ColourCommandHandler.UpdateColourAsync(ColourModel, true);
            Snackbar.Add($"Colour {ColourModel.Name} successfully updated.", Severity.Success);
            NavigationManager.NavigateTo("/colours/index");
        }
        catch (Exception ex)
        {
            Snackbar.Add($"An error occurred updating colour {ColourModel.Name}. Please try again.", Severity.Error);
            await ErrorCommandHandler.CreateErrorAsync(ex, true);
        }
    }
}
