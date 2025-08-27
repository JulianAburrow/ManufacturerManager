namespace MMUserInterface.Components.Pages.Admin.ColourJustifications;

public partial class Create
{
    protected override void OnInitialized()
    {
        MainLayout.SetHeaderValue("Create Colour Justification");
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetColourJustificationHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(CreateTextForBreadcrumb),
        ]);
    }

    private async Task CreateColourJustification()
    {
        CopyDisplayModelToModel();

        var actionSuccessful = await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await ColourJustificationCommandHandler.CreateColourJustificationAsync(ColourJustificationModel, true),
            $"Colour Justification {ColourJustificationModel.Justification} successfully created.",
            $"An error occurred creating Colour Justification {ColourJustificationModel.Justification}. Please try again"
        );

        if (actionSuccessful)
            NavigationManager.NavigateTo("/colourjustifications/index");
    }
}
