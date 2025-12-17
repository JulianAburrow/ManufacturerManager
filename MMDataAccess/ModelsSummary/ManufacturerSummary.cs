namespace MMDataAccess.ModelsSummary;

public class ManufacturerSummary
{
    public required int ManufacturerId { get; init; }

    public required string Name { get; init; }

    public string? StatusName { get; init; }

    public int WidgetCount { get; init; }

}
