namespace MMUserInterface.Shared.Components;

public partial class WidgetCreateUpdateComponent
{
    [Inject] protected IManufacturerQueryHandler ManufacturerHandler { get; set; } = default!;

    [Parameter] public WidgetDisplayModel WidgetDisplayModel { get; set; } = null!; 

    [Parameter] public List<WidgetStatusModel> WidgetStatuses { get; set; } = null!;

    [Parameter] public List<ColourModel> Colours { get; set; } = null!;

    [Parameter] public List<ColourJustificationModel> ColourJustifications { get; set; } = null!;

    [Parameter] public List<ManufacturerSummary> Manufacturers { get; set; } = null!;

    private bool ManufacturerIsInactive;

    protected override async Task OnInitializedAsync()
    {
        await SetWidgetStatusId();
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