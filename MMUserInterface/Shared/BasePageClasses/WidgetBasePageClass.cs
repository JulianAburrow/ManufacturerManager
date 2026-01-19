namespace MMUserInterface.Shared.BasePageClasses;

public abstract class WidgetBasePageClass : BasePageClass
{
    [Inject] protected IWidgetQueryHandler WidgetQueryHandler { get; set; } = default!;

    [Inject] protected IWidgetCommandHandler WidgetCommandHandler { get; set; } = default!;

    [Inject] protected IWidgetStatusQueryHandler WidgetStatusQueryHandler { get; set; } = default!;

    [Inject] protected IColourQueryHandler ColourHandler { get; set; } = default!;

    [Inject] protected IColourJustificationQueryHandler ColourJustificationHandler { get; set; } = default!;

    [Inject] protected IManufacturerQueryHandler ManufacturerHandler { get; set; } = default!;

    [Parameter] public int WidgetId { get; set; }

    protected WidgetModel WidgetModel = new();

    protected WidgetDisplayModel WidgetDisplayModel = new();

    protected List<WidgetStatusModel>? WidgetStatuses { get; set; }

    protected List<ColourModel>? Colours { get; set; }

    protected List<ColourJustificationModel>? ColourJustifications { get; set; }

    protected List<ManufacturerSummary>? Manufacturers { get; set; }

    protected string FileName = string.Empty;

    protected string Widget = "Widget";

    protected string WidgetPlural = "Widgets";

    protected bool ManufacturerIsInactive;

    protected void CopyDisplayModelToModel()
    {
        WidgetModel.Name = WidgetDisplayModel.Name;
        WidgetModel.ManufacturerId = WidgetDisplayModel.ManufacturerId;
        WidgetModel.ColourId = WidgetDisplayModel.ColourId != SharedValues.NoneValue
            ? WidgetDisplayModel.ColourId
            : null;
        WidgetModel.ColourJustificationId = WidgetDisplayModel.ColourJustificationId != SharedValues.NoneValue
            ? WidgetDisplayModel.ColourJustificationId
            : null;
        WidgetModel.StatusId = WidgetDisplayModel.StatusId;
        WidgetModel.CostPrice = WidgetDisplayModel.CostPrice;
        WidgetModel.RetailPrice = WidgetDisplayModel.RetailPrice;
        WidgetModel.StockLevel = WidgetDisplayModel.StockLevel;
    }

    protected void CopyModelToDisplayModel()
    {
        WidgetDisplayModel.WidgetId = WidgetId;
        WidgetDisplayModel.Name = WidgetModel.Name;
        WidgetDisplayModel.ManufacturerId = WidgetModel.ManufacturerId;
        WidgetDisplayModel.ColourId = WidgetModel.ColourId != null
            ? WidgetModel.ColourId
            : SharedValues.NoneValue;
        WidgetDisplayModel.ColourJustificationId = WidgetModel.ColourJustificationId != null
            ? WidgetModel.ColourJustificationId
            : SharedValues.NoneValue;
        WidgetDisplayModel.StatusId = WidgetModel.StatusId;
        WidgetDisplayModel.Manufacturer = WidgetModel.Manufacturer;
        WidgetDisplayModel.WidgetImage = WidgetModel.WidgetImage;
        WidgetDisplayModel.CostPrice = WidgetModel.CostPrice;
        WidgetDisplayModel.RetailPrice = WidgetModel.RetailPrice;
        WidgetDisplayModel.StockLevel = WidgetModel.StockLevel;
    }

    protected BreadcrumbItem GetWidgetHomeBreadcrumbItem(bool isDisabled = false)
    {
        return new BreadcrumbItem(WidgetPlural, "/widgets/index", isDisabled);
    }
}
