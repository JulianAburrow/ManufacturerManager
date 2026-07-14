namespace MMUserInterface.Components.Pages.Manufacturers;

public partial class Edit
{
    protected override async Task OnInitializedAsync()
    {
        ManufacturerStatuses = await ManufacturerStatusQueryHandler.GetManufacturerStatusesAsync();
        ManufacturerModel = await ManufacturerQueryHandler.GetManufacturerAsync(ManufacturerId);

        if (ManufacturerModel.ManufacturerId == 0)
        {
            MainLayout.SetHeaderValue(ManufacturerNotFoundMessage);
            _entityNotFound = true;
            return;
        }

        ManufacturerDisplayModel.ManufacturerId = ManufacturerId;
        ManufacturerDisplayModel.Name = ManufacturerModel.Name;
        ManufacturerDisplayModel.StatusId = ManufacturerModel.StatusId;
        MainLayout.SetHeaderValue("Edit Manufacturer");

        _isLoaded = true;
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
            async () => await ManufacturerCommandHandler.UpdateManufacturerAsync(ManufacturerModel),
            $"Manufacturer {ManufacturerModel.Name} successfully updated.",
            $"An error occurred updating manufacturer {ManufacturerModel.Name}. Please try again."
        );

        if (actionSuccessful)
            NavigationManager.NavigateTo("/manufacturers/index");
    }
}