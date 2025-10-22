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

    public required List<WidgetStatusModel> WidgetStatuses { get; set; }

    public required List<ColourModel> Colours { get; set; }

    public required List<ColourJustificationModel> ColourJustifications { get; set; }

    public required List<ManufacturerModel> Manufacturers { get; set; }

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

    protected BreadcrumbItem GetWidgetHomeBreadcrumbItem(bool isDisabled = false)
    {
        return new BreadcrumbItem(WidgetPlural, "/widgets/index", isDisabled);
    }

    protected async Task SetWidgetStatusId()
    {
        if (WidgetDisplayModel.ManufacturerId == SharedValues.PleaseSelectValue)
        {
            WidgetDisplayModel.StatusId = SharedValues.PleaseSelectValue;
            ManufacturerIsInactive = false;
            return;
        }
        var manufacturerStatusId = await ManufacturerHandler.GetManufacturerStatusByManufacturerId(WidgetDisplayModel.ManufacturerId);
        ManufacturerIsInactive = manufacturerStatusId == (int)ManufacturerStatusEnum.Inactive;
        WidgetDisplayModel.StatusId = ManufacturerIsInactive
            ? (int)WidgetStatusEnum.Inactive
            : SharedValues.PleaseSelectValue;
    }
}
