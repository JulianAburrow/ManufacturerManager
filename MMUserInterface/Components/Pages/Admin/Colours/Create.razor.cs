using MMUserInterface.Helpers;

namespace MMUserInterface.Components.Pages.Admin.Colours;

public partial class Create
{
    protected override void OnInitialized()
    {
        MainLayout.SetHeaderValue("Create Colour");
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetColourHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(CreateTextForBreadcrumb),
        ]);
    }

    private async Task CreateColour()
    {
        CopyDisplayModelToModel();

        await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await ColourCommandHandler.CreateColourAsync(ColourModel, true),
            $"Colour {ColourModel.Name} successfully created.",
            $"An error occurred creating colour {ColourModel.Name}. Please try again."
        );

        NavigationManager.NavigateTo("/colours/index");
    }
}
