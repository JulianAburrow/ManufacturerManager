namespace MMUserInterface.Components.Pages.Manufacturers;

public partial class Create
{
    protected override async Task OnInitializedAsync()
    {
        ManufacturerStatuses = await ManufacturerStatusQueryHandler.GetManufacturerStatusesAsync();
        ManufacturerStatuses.Insert(0, new ManufacturerStatusModel
        {
            StatusId = SharedValues.PleaseSelectValue,
            StatusName = SharedValues.PleaseSelectText,
        });
        ManufacturerDisplayModel.StatusId = -1;
        MainLayout.SetHeaderValue("Create Manufacturer");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(
        [
            GetHomeBreadcrumbItem(),
            GetManufacturerHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(CreateTextForBreadcrumb)
        ]);
    }

    private async Task CreateManufacturer()
    {
        CopyDisplayModelToModel();

        var actionSuccessful = await CrudWithErrorHandlingHelper.ExecuteWithErrorHandling(
            async () => await ManufacturerCommandHandler.CreateManufacturerAsync(ManufacturerModel, true),
            $"Manufacturer {ManufacturerModel.Name} successfully created.",
            $"An error occurred creating manufacturer {ManufacturerModel.Name}. Please try again."
        );

        if (actionSuccessful)
            NavigationManager.NavigateTo("/manufacturers/index");
    }
}