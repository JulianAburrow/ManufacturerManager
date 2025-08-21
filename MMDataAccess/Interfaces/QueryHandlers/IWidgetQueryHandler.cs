namespace MMDataAccess.Interfaces.QueryHandlers;

public interface IWidgetQueryHandler
{
    Task<WidgetModel> GetWidgetAsync(int widgetId);

    Task<List<WidgetModel>> GetWidgetsAsync();
}
