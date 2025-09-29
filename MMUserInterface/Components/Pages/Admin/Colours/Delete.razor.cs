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
        await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await ColourCommandHandler.DeleteColourAsync(ColourId, true),
            $"Colour {ColourModel.Name} successfully deleted.",
            $"An error occurred deleting colour {ColourModel.Name}. Please try again."
        );

        NavigationManager.NavigateTo("/colours/index");
    }
}
