namespace MMUserInterface.Components.Pages.Admin.ColourJustifications;

public partial class Delete
{
    protected override async Task OnInitializedAsync()
    {
        ColourJustificationModel = await ColourJustificationQueryHandler.GetColourJustificationAsync(ColourJustificationId);
        MainLayout.SetHeaderValue("Delete Colour Justification");
        OkToDelete = ColourJustificationModel.Widgets.Count == 0;
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetColourJustificationHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(DeleteTextForBreadcrumb),
        ]);
    }


    private async Task DeleteColourJustification()
    {
        var actionSuccessful = await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await ColourJustificationCommandHandler.DeleteColourJustificationAsync(ColourJustificationId, true),
            $"Colour Justification {ColourJustificationModel.Justification} successfully deleted.",
            $"An error occurred deleting Colour Justification {ColourJustificationModel.Justification}. Please try again."
        );

        if (actionSuccessful)
            NavigationManager.NavigateTo("/colourjustifications/index");
    }
}
