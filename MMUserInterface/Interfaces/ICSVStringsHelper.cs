namespace MMUserInterface.Interfaces;

public interface ICSVStringHelper
{
    string CreateWidgetCSVString(List<WidgetModel> widgets);

    string CreateManufacturerCSVString(List<ManufacturerModel> manufacturers);
}
