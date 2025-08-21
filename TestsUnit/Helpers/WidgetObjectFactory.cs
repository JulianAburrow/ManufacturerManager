namespace TestsUnit.Helpers;

public static class WidgetObjectFactory
{
    public static List<WidgetModel> GetTestWidgets()
    {
        var manufacturer1 = new ManufacturerModel
        {
            Name = "Manufacturer1",
            StatusId = (int)PublicEnums.ManufacturerStatusEnum.Active
        };

        var manufacturer2 = new ManufacturerModel
        {
            Name = "Manufacturer2",
            StatusId = (int)PublicEnums.ManufacturerStatusEnum.Inactive
        };

        var widgetStatus1 = new WidgetStatusModel
        {
            StatusId = (int)PublicEnums.WidgetStatusEnum.Active,
            StatusName = PublicEnums.WidgetStatusEnum.Active.ToString()
        };

        var widgetStatus2 = new WidgetStatusModel
        {
            StatusId = (int)PublicEnums.WidgetStatusEnum.Inactive,
            StatusName = PublicEnums.WidgetStatusEnum.Inactive.ToString()
        };

        return
        [
            new WidgetModel
            {
                Name = "Widget1",
                Manufacturer = manufacturer1,
                StatusId = (int)PublicEnums.WidgetStatusEnum.Active,
                Status = widgetStatus1,
            },
            new WidgetModel
            {
                Name = "Widget2",
                Manufacturer = manufacturer2,
                StatusId = (int)PublicEnums.WidgetStatusEnum.Inactive,
                Status = widgetStatus2,
            }
        ];
    }
}
