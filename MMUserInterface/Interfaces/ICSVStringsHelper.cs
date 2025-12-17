namespace MMUserInterface.Interfaces;

public interface ICSVStringHelper
{
    string CreateWidgetCSVString(List<WidgetSummary> widgets);

    string CreateManufacturerCSVString(List<ManufacturerSummary> manufacturers);
}
