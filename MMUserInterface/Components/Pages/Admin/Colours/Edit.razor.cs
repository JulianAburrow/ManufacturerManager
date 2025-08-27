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

       await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await ColourCommandHandler.UpdateColourAsync(ColourModel, true),
            $"Colour {ColourModel.Name} successfully updated.",
            $"An error occurred updating colour {ColourModel.Name}. Please try again."
        );

        NavigationManager.NavigateTo("/colours/index");
    }
}
