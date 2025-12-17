namespace MMDataAccess.ModelsSummary;

public class WidgetSummary
{
    public int WidgetId { get; set; }

    public required string Name { get; init; }

    public string? ManufacturerName { get; set; }

    public string? ColourName { get; set; }

    public decimal CostPrice { get; set; }

    public int StockLevel { get; set; }

    public string? StatusName { get; set; }
}
