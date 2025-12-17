namespace MMUserInterface.Components.Pages.Manufacturers;

public partial class View
{
    private List<WidgetSummary> WidgetsForManufacturer { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        ManufacturerModel = await ManufacturerQueryHandler.GetManufacturerAsync(ManufacturerId);
        foreach (var widget in ManufacturerModel.Widgets)
        {
            WidgetsForManufacturer.Add(new WidgetSummary
            {
                WidgetId = widget.WidgetId,
                Name = widget.Name,
                ManufacturerName = ManufacturerModel.Name,
                ColourName = widget.Colour?.Name ?? "Not Specified",
                CostPrice = widget.CostPrice,
                StockLevel = widget.StockLevel,
                StatusName = widget.Status.StatusName,
            });
        }
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