using System.Runtime.CompilerServices;

namespace MMUserInterface.Helpers;

public class CSVStringHelper : ICSVStringHelper
{
    public string CreateWidgetCSVString(List<WidgetSummary> widgets)
    {
        var widgetCSVSB = new StringBuilder();
        var totalStockValue = 0M;

        widgetCSVSB.AppendLine("Widget Id,Name,Manufacturer,Colour,Cost Price,Stock Level,Net Stock Value, Status");

        foreach (var widget in widgets)
        {
            var widgetStockValue = widget.CostPrice * widget.StockLevel;
            widgetCSVSB.AppendLine($"{widget.WidgetId},{CSVStrings.EscapeCsv(widget.Name)},{CSVStrings.EscapeCsv(widget.ManufacturerName)},{widget.ColourName},{widget.CostPrice},{widget.StockLevel},{widgetStockValue},{widget.StatusName}");
            totalStockValue += widgetStockValue;
        }

        widgetCSVSB.AppendLine($",,,,Total,{totalStockValue},");

        return widgetCSVSB.ToString();
    }

    public string CreateManufacturerCSVString(List<ManufacturerSummary> manufacturers)
    {
        var manufacturerCSVSB = new StringBuilder();
        manufacturerCSVSB.AppendLine("Manufacturer Id,Name,Status,Widget Count");
        foreach (var manufacturer in manufacturers)
        {
            manufacturerCSVSB.AppendLine($"{manufacturer.ManufacturerId},{CSVStrings.EscapeCsv(manufacturer.Name)},{manufacturer.StatusName},{manufacturer.WidgetCount}");
        }

        return manufacturerCSVSB.ToString();
    }
}
