namespace MMDataAccess.Interfaces.CommandHandlers;

public interface IWidgetCommandHandler
{
    Task CreateWidgetAsync(WidgetModel widget);

    Task UpdateWidgetAsync(WidgetModel widget);

    Task DeleteWidgetAsync(int widgetId);
}
