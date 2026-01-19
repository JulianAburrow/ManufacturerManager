namespace MMUserInterface.Shared.BasePageClasses;

public abstract class ManufacturerBasePageClass : BasePageClass
{
    [Inject] protected IManufacturerQueryHandler ManufacturerQueryHandler { get; set; } = default!;

    [Inject] protected IManufacturerCommandHandler ManufacturerCommandHandler { get; set; } = default!;

    [Inject] protected IManufacturerStatusQueryHandler ManufacturerStatusQueryHandler { get; set; } = default!;

    [Parameter] public int ManufacturerId { get; set; }

    protected ManufacturerModel ManufacturerModel = new();

    protected ManufacturerDisplayModel ManufacturerDisplayModel = new();

    protected List<ManufacturerStatusModel>? ManufacturerStatuses { get; set; }

    protected string Manufacturer = "Manufacturer";

    protected string ManufacturerPlural = "Manufacturers";

    protected void CopyDisplayModelToModel()
    {
        ManufacturerModel.Name = ManufacturerDisplayModel.Name;
        ManufacturerModel.StatusId = ManufacturerDisplayModel.StatusId;
    }

    protected BreadcrumbItem GetManufacturerHomeBreadcrumbItem(bool isDisabled = false)
    {
        return new BreadcrumbItem(ManufacturerPlural, "/manufacturers/index", isDisabled);
    }
}
