namespace TestsPlaywright.Helpers;

public static class WidgetHelper
{
    public static int AddWidget(int manufacturerId, ManufacturerManagerContext context)
    {
        var newWidget = new WidgetModel
        {
            Name = $"Test Widget {Guid.NewGuid()}",
            ManufacturerId = manufacturerId,
            StatusId = (int)PublicEnums.WidgetStatusEnum.Active,
        };
        context.Widgets.Add(newWidget);
        context.SaveChanges();
        return newWidget.WidgetId;
    }

    public static void RemoveWidget(int widgetId, ManufacturerManagerContext context)
    {
        var widget = context.Widgets.Find(widgetId);
        if (widget != null)
        {
            context.Widgets.Remove(widget);
            context.SaveChanges();
        }
    }
}
