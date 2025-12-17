namespace MMUserInterface.Shared.Components;

public partial class ManufacturerGridViewComponent
{
    [Parameter] public List<ManufacturerSummary> Manufacturers { get; set; } = null!;
}