namespace MMUserInterface.Components.Pages.Manufacturers;

public partial class View
{
    protected override async Task OnInitializedAsync()
    {
        ManufacturerModel = await ManufacturerQueryHandler.GetManufacturerAsync(ManufacturerId);
        MainLayout.SetHeaderValue("View Manufacturer");
    }

    protected override void OnInitialized()
    {
        MainLayout.SetBreadcrumbs(new List<BreadcrumbItem>
        {
            GetHomeBreadcrumbItem(),
            GetManufacturerHomeBreadcrumbItem(),
            GetCustomBreadcrumbItem(ViewTextForBreadcrumb),
        });
    }
}