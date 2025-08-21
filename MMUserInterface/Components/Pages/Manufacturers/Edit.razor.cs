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
        try
        {
            CopyDisplayModelToModel();
            await ManufacturerCommandHandler.UpdateManufacturerAsync(ManufacturerModel, true);
            Snackbar.Add($"Manufacturer {ManufacturerModel.Name} successfully updated.", Severity.Success);
            NavigationManager.NavigateTo("/manufacturers/index");
        }
        catch (Exception ex)
        {
            Snackbar.Add($"An error occurred updating manufacturer {ManufacturerModel.Name}. Please try again.", Severity.Error);
            await ErrorCommandHandler.CreateErrorAsync(ex, true);
        }
    }
}