namespace MMUserInterface.Components.Pages.Manufacturers;

public partial class Edit
{
    protected override async Task OnInitializedAsync()
    {
        ManufacturerStatuses = await ManufacturerStatusQueryHandler.GetManufacturerStatusesAsync();
        ManufacturerModel = await ManufacturerQueryHandler.GetManufacturerAsync(ManufacturerId);
        ManufacturerDisplayModel.ManufacturerId = ManufacturerId;
        ManufacturerDisplayModel.Name = ManufacturerModel.Name;
        ManufacturerDisplayModel.StatusId = ManufacturerModel.StatusId;
        MainLayout.SetHeaderValue("Edit Manufacturer");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetManufacturerHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(EditTextForBreadcrumb),
        ]);
    }

    private async Task UpdateManufacturer()
    {
        CopyDisplayModelToModel();

        var actionSuccessful = await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await ManufacturerCommandHandler.UpdateManufacturerAsync(ManufacturerModel, true),
            $"Manufacturer {ManufacturerModel.Name} successfully updated.",
            $"An error occurred updating manufacturer {ManufacturerModel.Name}. Please try again."
        );

        if (actionSuccessful)
            NavigationManager.NavigateTo("/manufacturers/index");
    }
}