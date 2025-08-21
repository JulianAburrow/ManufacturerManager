namespace MMDataAccess.Interfaces.CommandHandlers;

public interface IWidgetCommandHandler
{
    Task CreateWidgetAsync(WidgetModel widget, bool callSaveChanges);

    Task UpdateWidgetAsync(WidgetModel widget, bool callSaveChanges);

    Task DeleteWidgetAsync(int widgetId, bool callSaveChanges);

    Task SaveChangesAsync();
}
