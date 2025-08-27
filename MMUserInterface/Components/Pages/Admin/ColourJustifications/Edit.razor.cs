namespace MMUserInterface.Components.Pages.Admin.ColourJustifications;

public partial class Edit
{
    protected override async Task OnInitializedAsync()
    {
        ColourJustificationModel = await ColourJustificationQueryHandler.GetColourJustificationAsync(ColourJustificationId);
        ColourJustificationDisplayModel.Justification = ColourJustificationModel.Justification;
        MainLayout.SetHeaderValue("Edit Colour Justification");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetColourJustificationHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(EditTextForBreadcrumb),
        ]);
    }

    private async Task UpdateColourJustification()
    {
        CopyDisplayModelToModel();

        var actionSuccessful = await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await ColourJustificationCommandHandler.UpdateColourJustificationAsync(ColourJustificationModel, true),
            $"Colour Justification {ColourJustificationModel.Justification} successfully updated.",
            $"An error occurred updating Colour Justification {ColourJustificationModel.Justification}. Please try again."
        );

        if (actionSuccessful)
            NavigationManager.NavigateTo("/colourjustifications/index");
    }
}
